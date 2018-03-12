using System;
using System.Threading.Tasks;
using DMCTS.GrainInterfaces;
using Orleans;

namespace DMCTS.Grains
{
    public class TreeGrain<TAction> : Grain, ITreeGrain<TAction> where TAction : IAction
    {
        private INodeGrain<TAction> _root;

        public async Task Init(IState<TAction> initialState)
        {
            DelayDeactivation(TimeSpan.FromMinutes(30));
            _root = GrainFactory.GetGrain<INodeGrain<TAction>>(Guid.NewGuid());
            await _root.Init(Guid.Empty, initialState, default);
        }

        public Task ContinueFrom(Guid nodeId)
        {
            _root = GrainFactory.GetGrain<INodeGrain<TAction>>(nodeId);
            _root.Init(Guid.Empty);
            return Task.CompletedTask;
        }

        public Task<INodeView<TAction>> GetTopAction()
        {
            return _root.GetTopAction();
        }

        public async Task Build()
        {
            for (var i = 0; i < 1000; i++)
            {
                await _root.Build();
            }
        }
    }
}