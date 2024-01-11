namespace Common.States
{
    public interface IGameStateMachine
    {
        void StartState<TState>() where TState : class, IState;
        void StartState<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}