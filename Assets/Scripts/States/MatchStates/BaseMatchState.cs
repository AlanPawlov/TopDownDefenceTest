using System;

namespace States.MatchStates
{
    public abstract class BaseMatchState : IState, IDisposable
    {
        protected IGameStateMachine _stateMachine;

        public BaseMatchState(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public abstract void StartState();
        public abstract void StopState();

        public virtual void Dispose()
        {
            _stateMachine = null;
        }
    }
}