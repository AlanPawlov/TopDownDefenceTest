namespace Events.Handlers
{
    public interface IWallDamageHandler : IGlobalSubscriber
    {
        void HandleWallDamage(int health);
    }
}