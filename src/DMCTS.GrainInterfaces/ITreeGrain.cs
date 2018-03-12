using System;
using System.Threading.Tasks;
using Orleans;

namespace DMCTS.GrainInterfaces
{
    public interface ITreeGrain<TAction> : IGrainWithGuidKey where TAction : IAction
    {
        Task Init(IState<TAction> initialState);
        Task ContinueFrom(Guid nodeId);
        Task Build();
        Task<INodeView<TAction>> GetTopAction();
    }
}