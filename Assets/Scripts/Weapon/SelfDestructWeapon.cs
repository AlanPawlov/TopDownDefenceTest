using Interfaces;
using Models;

namespace Weapon
{
    public class SelfDestructWeapon : BaseWeapon
    {
        public SelfDestructWeapon(Character owner, WeaponModel weaponModel) : base(owner)
        {
            _attackDistance = weaponModel.Range;
            _damage = weaponModel.Damage;
        }

        public override void Attack(IDamageable target)
        {
            target.ApplyDamage(Damage);
            _owner.Death();
        }
    }
}