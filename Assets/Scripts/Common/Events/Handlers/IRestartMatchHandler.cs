namespace Common.Events.Handlers
{
    public interface IRestartMatchHandler : IGlobalSubscriber
    {
        void HandleRestart();
    }
}