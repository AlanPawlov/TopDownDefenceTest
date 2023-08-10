using System.Threading.Tasks;
using Events.Handlers;
using Interfaces;
using Models;
using Services;
using UnityEngine;
using Zenject;
using IPoolable = UI.IPoolable;
using Random = UnityEngine.Random;


namespace Character
{
    public class Character : MonoBehaviour, IDamageable, IMovable, IAttackable, IPoolable
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Animator _animator;
        [Inject] private UpdateSender _updateSender;
        private IWeapon _weapon;
        private int _isMovingHashedParameter;
        private int _health;
        private float _speed;
        private bool _canWalk;
        public int Health => _health;
        public bool CanWalk => _canWalk;
        public float Speed => _speed;
        public IWeapon Weapon => _weapon;
        public Transform Transform => transform;
        public string ResourceName { get; set; }

        public void Setup(IWeapon weapon, CharacterModel model)
        {
            _health = model.Health;
            _speed = Random.Range(model.MinSpeed, model.MaxSpeed);
            _weapon = weapon;
            _renderer.sprite = model.CharacterView;
            _animator.runtimeAnimatorController = model.AnimatorController;
            _isMovingHashedParameter = Animator.StringToHash("IsMoving");
            _updateSender.OnUpdate += OnUpdate;
        }

        private void OnUpdate()
        {
            _weapon.OnUpdate();
        }

        public void ApplyDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Death();
                Events.EventBus.RaiseEvent<IKillCharacterHandler>(h => h.HandleKillCharacter());
            }
        }

        public void Death()
        {
            _health = 0;
            Events.EventBus.RaiseEvent<IDeathHandler>(h => h.HandleDeath(this));
            Uninit();
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void Move(Vector2 direction)
        {
            var isMoving = direction != Vector2.zero;
            _animator.SetBool(_isMovingHashedParameter, isMoving);
            _rigidbody.velocity = direction * _speed;
        }


        public void Attack(IDamageable target)
        {
            _weapon.Attack(target);
        }

        public async Task Init()
        {
        }

        public void Uninit()
        {
            _updateSender.OnUpdate -= OnUpdate;
            _health = 0;
            _canWalk = false;
            _speed = 0;
        }

        private void OnDestroy()
        {
            Uninit();
        }
    }
}