using System;

namespace States.SceneStates
{
    public abstract class BaseSceneState : IState, IDisposable
    {
        protected IGameStateMachine StateMachine;

        public BaseSceneState(IGameStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract void EnterState();
        public abstract void ExitState();

        public virtual void Dispose()
        {
            StateMachine = null;
        }
    }
}