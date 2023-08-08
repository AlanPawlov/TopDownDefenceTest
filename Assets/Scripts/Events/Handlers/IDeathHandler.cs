namespace Events.Handlers
{
    public interface IDeathHandler : IGlobalSubscriber
    {
        void HandleDeath(Character character);
    } 
    
    public interface IKillCharacterHandler : IGlobalSubscriber
    {
        void HandleKillCharacter();
    }

    public interface IWallDestroyedHandler : IGlobalSubscriber
    {
        void HandleWallDestroyed();
    }
    
    public interface IWallDamageHandler : IGlobalSubscriber
    {
        void HandleWallDamage(int health);
    } 
    
    public interface IRestartMatchHandler : IGlobalSubscriber
    {
        void HandleRestart();
    }
}