using Factories;
using Interfaces;
using Models;
using Pools;
using UnityEngine;
using Zenject;

namespace Weapon
{
    public class ProjectileWeapon : BaseWeapon
    {
        private float _bulletSpeed;
        private float _cooldownTime;
        private float _cooldownTimer;
        private string _bulletPath;
        [Inject] private ProjectileFactory _factory;
        [Inject] private ProjectilePool _pool;

        public ProjectileWeapon(Character.Character owner, WeaponModel weaponModel, ProjectileModel projectileModel) : base(owner)
        {
            _attackDistance = weaponModel.Range;
            _damage = weaponModel.Damage;
            _cooldownTime = 1 / weaponModel.AttackSpeed;
            _bulletSpeed = projectileModel.BulletSpeed;
            _bulletPath = projectileModel.ResourcePath;
            _canAtttack = true;
        }

        public override void OnUpdate()
        {
            _cooldownTimer += Time.deltaTime;
            if (_cooldownTimer >= _cooldownTime)
            {
                _canAtttack = true;
            }
        }

        public override async void Attack(IDamageable target)
        {
            if (!_canAtttack)
                return;
            _canAtttack = false;
            _cooldownTimer = 0;
            var ownerPosition = _owner.GetPosition();
            var direction = (target.GetPosition() - ownerPosition).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var bullet = (Projectile)_pool.LoadFromPool<Projectile>(_bulletPath, _owner.GetPosition(),
                Quaternion.Euler(0.0f, 0.0f, angle));
            if (bullet == null)
                bullet = await _factory.Create(_bulletPath, _owner.GetPosition(),
                    Quaternion.Euler(0.0f, 0.0f, angle));
            bullet.Setup(_damage, _bulletSpeed, _owner);
        }
    }
}