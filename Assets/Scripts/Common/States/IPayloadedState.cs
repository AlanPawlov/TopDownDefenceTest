
namespace Common.States
{
    public interface IPayloadedState<TPayload> : IState
    {
        void StartState(TPayload payload);
    }
}