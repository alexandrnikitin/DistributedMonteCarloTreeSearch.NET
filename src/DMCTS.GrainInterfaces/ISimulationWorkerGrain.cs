using System.Threading.Tasks;
using Orleans;

namespace DMCTS.GrainInterfaces
{
    public interface ISimulationWorkerGrain<TAction> : IGrainWithIntegerKey where TAction : IAction
    {
        Task<double> Simulate(IState<TAction> state);
    }
}