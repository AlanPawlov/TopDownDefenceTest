namespace Events.Handlers
{
    public interface IDeathHandler : IGlobalSubscriber
    {
        void HandleDeath(Character.Character character);
    }
}