namespace Services.MatchStates
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : BaseState;
    }
}