using System;
using System.Collections.Immutable;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DMCTS.GrainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;

namespace Rides.Client
{
    class Program
    {
        static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                using (var client = await StartClientWithRetries())
                {
                    await DoClientWork(client);
                    Console.ReadKey();
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                return 1;
            }
        }

        private static async Task<IClusterClient> StartClientWithRetries(int initializeAttemptsBeforeFailing = 5)
        {
            int attempt = 0;
            IClusterClient client;
            while (true)
            {
                try
                {
                    var siloAddress = IPAddress.Loopback;
                    var gatewayPort = 30000;
                    client = new ClientBuilder()
                        .ConfigureCluster(options => options.ClusterId = "google-hashcode-2018")
                        .UseStaticClustering(options => options.Gateways.Add((new IPEndPoint(siloAddress, gatewayPort)).ToGatewayUri()))
                        .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(ITreeGrain<>).Assembly).WithReferences())
                        .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(MakeRideAction).Assembly).WithReferences())
                        .ConfigureLogging(logging => logging.AddConsole())
                        .Build();

                    await client.Connect();
                    Console.WriteLine("Client successfully connect to silo host");
                    break;
                }
                catch (SiloUnavailableException)
                {
                    attempt++;
                    Console.WriteLine($"Attempt {attempt} of {initializeAttemptsBeforeFailing} failed to initialize the Orleans client.");
                    if (attempt > initializeAttemptsBeforeFailing)
                    {
                        throw;
                    }
                    await Task.Delay(TimeSpan.FromSeconds(4));
                }
            }

            return client;
        }

        private static async Task DoClientWork(IClusterClient client)
        {
            var problem = ProblemBuilder.Build(File.ReadAllLines(@"..\Resources\a_example.in"));
            var solution = new Solution(problem.NumberOfCars);
            var state = new CityState(problem.Cars.ToImmutableList(), new RidesView3(problem.Rides, problem.Bonus), 0);

            var tree = client.GetGrain<ITreeGrain<MakeRideAction>>(Guid.NewGuid());
            tree.Init(state).Wait();
            tree.Build().Wait();
            INodeView<MakeRideAction> node;
//            while ((node = tree.GetTopAction().Result) != null)
//            {
//                if (!node.Action.Car.Equals(Car.SkipRide))
//                {
//                    solution.CarActions[node.Action.Car.Id].Add(node.Action);
//                }
//
//                tree.ContinueFrom(node.Id).Wait();
//                tree.Build().Wait();
//            }

            Console.WriteLine("Finished");
            Console.WriteLine(solution.GetTotalScore(problem.Bonus).ToString());
            Console.WriteLine(solution.ToString());
        }



    }
}
