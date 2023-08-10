using System;

namespace Services.MatchStates
{
    public abstract class BaseState:IDisposable
    {
        protected IStateSwitcher _stateSwitcher;

        public BaseState(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public abstract void StartState();
        public abstract void StopState();
        public virtual void Dispose()
        {
            _stateSwitcher = null;
        }
    }
}