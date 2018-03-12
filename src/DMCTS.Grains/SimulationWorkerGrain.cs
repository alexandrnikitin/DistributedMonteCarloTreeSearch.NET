using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMCTS.GrainInterfaces;
using Orleans;
using Orleans.Concurrency;

namespace DMCTS.Grains
{
    [StatelessWorker]
    [Reentrant]
    public class SimulationWorkerGrain<TAction> : Grain, ISimulationWorkerGrain<TAction> where TAction : IAction
    {
        private readonly int _simulationSteps = 100;

        public async Task<double> Simulate(IState<TAction> state)
        {
            var counter = 0;
            while (state.GetAvailableActions().Any() && counter < _simulationSteps)
            {
                state = state.Apply(((List<TAction>)state.GetAvailableActions()).RandomChoice());
                counter++;
            }

            return state.GetScore();
        }
    }
}