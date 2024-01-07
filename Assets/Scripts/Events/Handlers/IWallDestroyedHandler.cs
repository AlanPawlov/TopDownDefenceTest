namespace Events.Handlers
{
    public interface IWallDestroyedHandler : IGlobalSubscriber
    {
        void HandleWallDestroyed();
    }
}