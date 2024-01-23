using Game.Character;

namespace CommonTemplate.Events.Handlers
{
    public interface ISpawnCharacterHandler : IGlobalSubscriber
    {
        void HandleSpawnEnemy(Character character);
        void HandleSpawnPlayer(Character character);
    }
}