using UnityEngine;

namespace Game.Character
{
    public interface IDamageable
    {
        public int Health { get; }
        public void ApplyDamage(int damage);
        public void Death();
        public Vector3 GetPosition();
    }
}