namespace CommonTemplate.Events.Handlers
{
    public interface IKillCharacterHandler : IGlobalSubscriber
    {
        void HandleKillCharacter();
    }
}