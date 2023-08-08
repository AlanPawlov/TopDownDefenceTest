namespace Services
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : BaseState;
    }
}