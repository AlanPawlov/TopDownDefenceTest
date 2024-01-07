namespace Events.Handlers
{
    public interface IKillCharacterHandler : IGlobalSubscriber
    {
        void HandleKillCharacter();
    }
}