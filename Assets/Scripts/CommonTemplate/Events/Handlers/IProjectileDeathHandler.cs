using Game.Weapon;

namespace CommonTemplate.Events.Handlers
{
    public interface IProjectileDeathHandler : IGlobalSubscriber
    {
        void HandleProjectileDeath(Projectile projectile);
    }
}