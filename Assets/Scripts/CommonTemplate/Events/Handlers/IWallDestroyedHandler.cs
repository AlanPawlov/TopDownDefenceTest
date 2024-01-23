namespace CommonTemplate.Events.Handlers
{
    public interface IWallDestroyedHandler : IGlobalSubscriber
    {
        void HandleWallDestroyed();
    }
}