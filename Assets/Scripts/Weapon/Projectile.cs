using Interfaces;
using UnityEngine;

namespace Weapon
{ 
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float Speed;
        [SerializeField] private int Damage;
        [SerializeField] private bool _isAlive;
        private Collider2D _ownerCollider;

        public void Setup(int damage, float speed, Character character)
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

            IDamageable target;
            if (col.gameObject.TryGetComponent<IDamageable>(out target))
            {
                target.ApplyDamage(Damage);
                _isAlive = false;
                Destroy(gameObject);
            }
        }
    }
}