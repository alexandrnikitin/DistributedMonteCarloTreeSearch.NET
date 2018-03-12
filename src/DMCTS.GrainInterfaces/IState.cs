using System.Collections.Generic;

namespace DMCTS.GrainInterfaces
{
    public interface IState<TAction> where TAction: IAction
    {
        IEnumerable<TAction> GetAvailableActions();
        IState<TAction> Apply(TAction action);
        double GetScore();
    }
}