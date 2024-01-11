using Game.Character;

namespace Common.Events.Handlers
{
    public interface ISpawnCharacterHandler : IGlobalSubscriber
    {
        void HandleSpawnEnemy(Character character);
        void HandleSpawnPlayer(Character character);
    }
}