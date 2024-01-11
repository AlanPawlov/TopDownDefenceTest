using Game.Character;
using Game.Models;

namespace Game.Weapon
{
    public class SelfDestructWeapon : BaseWeapon
    {
        public SelfDestructWeapon(global::Game.Character.Character owner, WeaponModel weaponModel) : base(owner)
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