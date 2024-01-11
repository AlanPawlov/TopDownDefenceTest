using Weapon;

namespace Common.Events.Handlers
{
    public interface IProjectileDeathHandler : IGlobalSubscriber
    {
        void HandleProjectileDeath(Projectile projectile);
    }
}