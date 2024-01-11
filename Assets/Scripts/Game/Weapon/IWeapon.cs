using Game.Character;

namespace Game.Weapon
{
    public interface IWeapon
    {
        public float AttackDistance { get; }
        public int Damage { get; }
        public bool CanAttack { get; }
        public void OnUpdate();
        public void Attack(IDamageable target);
    }
}