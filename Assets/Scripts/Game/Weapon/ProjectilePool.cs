using Common.Events;
using Common.Events.Handlers;
using Common.Pool;
using UnityEngine;
using Zenject;
using IPoolable = Common.Pool.IPoolable;

namespace Game.Weapon
{
    public class ProjectilePool : BasePool, IProjectileDeathHandler
    {
        public ProjectilePool(DiContainer container) : base(container)
        {
            EventBus.Subscribe(this);
        }

        public override IPoolable LoadFromPool<T>(string resourceName, Transform parent = null)
        {
            var obj = base.LoadFromPool<T>(resourceName, parent);
            return obj;
        }

        public IPoolable LoadFromPool<T>(string resourceName,
            Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
        {
            Projectile obj = (Projectile)LoadFromPool<T>(resourceName, null);

            if (obj != null)
            {
                var transform = obj.transform;
                transform.position = position;
                transform.localRotation = rotation;
            }

            return obj;
        }

        public void HandleProjectileDeath(Projectile projectile)
        {
            RemoveToPool(projectile);
        }

        public override void Dispose()
        {
            base.Dispose();
            EventBus.Unsubscribe(this);
        }
    }
}