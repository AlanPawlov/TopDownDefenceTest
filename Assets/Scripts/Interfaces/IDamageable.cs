namespace Interfaces
{
    public interface IDamageable
    {
        public int Health { get; }
        public void ApplyDamage(int damage);
        public void Death();
    }
}