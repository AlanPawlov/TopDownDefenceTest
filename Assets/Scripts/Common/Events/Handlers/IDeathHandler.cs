using Game.Character;

namespace Common.Events.Handlers
{
    public interface IDeathHandler : IGlobalSubscriber
    {
        void HandleDeath(Character character);
    }
}