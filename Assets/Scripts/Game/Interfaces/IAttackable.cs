namespace Interfaces
{
    public interface IAttackable
    {
        public IWeapon Weapon { get; }
        public void Attack(IDamageable target);
    }
}