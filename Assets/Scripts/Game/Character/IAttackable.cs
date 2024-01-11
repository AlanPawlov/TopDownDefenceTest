using Game.Weapon;

namespace Game.Character
{
    public interface IAttackable
    {
        public IWeapon Weapon { get; }
        public void Attack(IDamageable target);
    }
}