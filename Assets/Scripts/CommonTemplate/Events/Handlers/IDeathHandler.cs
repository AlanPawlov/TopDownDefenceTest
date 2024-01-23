using Game.Character;

namespace CommonTemplate.Events.Handlers
{
    public interface IDeathHandler : IGlobalSubscriber
    {
        void HandleDeath(Character character);
    }
}