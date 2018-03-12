using System;
using System.Threading.Tasks;
using Orleans;

namespace DMCTS.GrainInterfaces
{
    public interface INodeGrain<TAction> : IGrainWithGuidKey where TAction: IAction
    {
        Task Init(Guid parentId);
        Task Init(Guid parentId, IState<TAction> state, TAction action);
        Task Build();
        Task Expand();
        Task BackPropagate(INodeView<TAction> node);
        Task<INodeView<TAction>> GetTopAction();
    }
}