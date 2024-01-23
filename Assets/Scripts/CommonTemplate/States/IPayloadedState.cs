namespace CommonTemplate.States
{
    public interface IPayloadedState<TPayload> : IState
    {
        void EnterState(TPayload payload);
    }
}