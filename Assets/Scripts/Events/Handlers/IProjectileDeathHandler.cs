using Weapon;

namespace Events.Handlers
{
    public interface IProjectileDeathHandler : IGlobalSubscriber
    {
        void HandleProjectileDeath(Projectile projectile);
    }
}