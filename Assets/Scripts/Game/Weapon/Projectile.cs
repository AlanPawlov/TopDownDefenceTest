using System.Threading.Tasks;
using CommonTemplate.Events;
using CommonTemplate.Events.Handlers;
using CommonTemplate.Pool;
using Game.Character;
using UnityEngine;

namespace Game.Weapon
{
    public class Projectile : MonoBehaviour, IPoolable
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        private float Speed;
        private int Damage;
        private bool _isAlive;
        private Collider2D _ownerCollider;
        public Transform Transform => transform;
        public string ResourceName { get; set; }

        public async Task Init()
        {
        }

        public void Uninit()
        {
            _isAlive = false;
        }


        public void Setup(int damage, float speed, Character.Character character)
        {
            Damage = damage;
            Speed = speed;
            character.TryGetComponent(out _ownerCollider);
            _isAlive = true;
        }

        private void FixedUpdate()
        {
            if (!_isAlive)
                return;
            _rigidbody.velocity = transform.TransformDirection(Vector3.right) * Speed;
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!_isAlive || col == _ownerCollider)
                return;

            if (col.gameObject.TryGetComponent<IDamageable>(out var target))
                target.ApplyDamage(Damage);

            Uninit();
            EventBus.RaiseEvent<IProjectileDeathHandler>(h => h.HandleProjectileDeath(this));
        }
    }
}