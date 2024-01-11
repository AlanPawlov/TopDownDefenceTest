namespace Common.Events.Handlers
{
    public interface ISpawnCharacterHandler : IGlobalSubscriber
    {
        void HandleSpawnEnemy(Character.Character character);
        void HandleSpawnPlayer(Character.Character character);
    }
}