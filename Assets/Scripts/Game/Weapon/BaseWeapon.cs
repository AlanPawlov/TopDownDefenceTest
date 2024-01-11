using Game.Character;

namespace Game.Weapon
{
    public class BaseWeapon : IWeapon
    {
        protected readonly Character.Character _owner;
        protected float _attackDistance;
        protected int _damage;
        protected bool _canAtttack;
        public float AttackDistance => _attackDistance;
        public int Damage => _damage;
        public bool CanAttack => _canAtttack;

        public BaseWeapon(Character.Character owner)
        {
            _owner = owner;
            _canAtttack = true;
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void Attack(IDamageable target)
        {
        }
    }
}