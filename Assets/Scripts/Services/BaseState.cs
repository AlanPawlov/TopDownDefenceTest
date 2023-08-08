namespace Services
{
    public abstract class BaseState
    {
        protected readonly IStateSwitcher _stateSwitcher;

        public BaseState(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public abstract void StartState();
        public abstract void StopState();
    }
}