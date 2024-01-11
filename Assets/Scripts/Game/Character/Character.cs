using System.Threading.Tasks;
using Common.Events;
using Common.Events.Handlers;
using Interfaces;
using Models;
using Resource;
using Services;
using UnityEngine;
using UnityEngine.UI;
using Utils;
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
        private CharacterModel _model;
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

        public void SetupBaseStats(IWeapon weapon, CharacterModel model)
        {
            _model = model;
            _health = _model.Health;
            _speed = Random.Range(_model.MinSpeed, _model.MaxSpeed);
            _weapon = weapon;
            _updateSender.OnUpdate += OnUpdate;
        }

        public async void SetupView(IResourceLoader resourceLoader)
        {
            _renderer.sprite =  await resourceLoader.Load<Sprite>(_model.CharacterView.CollapseAddressablePath());
            _animator.runtimeAnimatorController = await resourceLoader.Load<AnimatorOverrideController>(_model.AnimatorController.CollapseAddressablePath());
            _isMovingHashedParameter = Animator.StringToHash("IsMoving");
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
                EventBus.RaiseEvent<IKillCharacterHandler>(h => h.HandleKillCharacter());
            }
        }

        public void Death()
        {
            _health = 0;
            EventBus.RaiseEvent<IDeathHandler>(h => h.HandleDeath(this));
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