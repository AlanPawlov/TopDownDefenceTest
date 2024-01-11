namespace Common.States
{
    public interface IState
    {
        void EnterState();
        void ExitState();
    }
}