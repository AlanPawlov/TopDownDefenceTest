using Interfaces;

namespace Weapon
{
    public class SelfDestructWeapon : BaseWeapon
    {
        public SelfDestructWeapon(Character owner) : base(owner)
        {
            _attackDistance = 0.5f;
        }

        public override void Attack(IDamageable target)
        {
            target.ApplyDamage(Damage);
            _owner.Death();
        }
    }
}