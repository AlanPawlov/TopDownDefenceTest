using Factories;
using Interfaces;
using Pools;
using UI;
using UnityEngine;
using Zenject;

namespace Weapon
{
    public class ProjectileWeapon : BaseWeapon
    {
        private float cooldownTime = 1.2f;
        private float cooldownTimer;
        [Inject] private ProjectileFactory _factory;
        [Inject] private ProjectilePool _pool;
        private string _bulletPath = "Prefabs/Bullet";

        public ProjectileWeapon(Character owner) : base(owner)
        {
            _attackDistance = 5f;
        }

        public override void OnUpdate()
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldownTime)
            {
                _canAtttack = true;
            }
        }

        public override async void Attack(IDamageable target)
        {
            if (!_canAtttack)
                return;
            _canAtttack = false;
            cooldownTimer = 0;
            var ownerPosition = _owner.GetPosition();
            var direction = (target.GetPosition() - ownerPosition).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var bullet = (Projectile)_pool.LoadFromPool<Projectile>(_bulletPath, _owner.GetPosition(),
                Quaternion.Euler(0.0f, 0.0f, angle));
            if (bullet == null)
                bullet = await _factory.Create(_bulletPath, _owner.GetPosition(),
                    Quaternion.Euler(0.0f, 0.0f, angle));
            bullet.Setup(1, 2.5f, _owner);
        }
    }
}