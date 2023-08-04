using System.Threading.Tasks;
using Events.Handlers;
using Interfaces;
using Services;
using UnityEngine;
using Zenject;
using IPoolable = UI.IPoolable;

public class Character : MonoBehaviour, IDamageable, IMovable, IAttackable, IPoolable
{
    [SerializeField] private Rigidbody2D _rigidbody;
    private int _health;
    private float _speed = 2.5f;
    private bool _canWalk;
    private IWeapon _weapon;
    public int Health => _health;
    public bool CanWalk => _canWalk;
    public float Speed => _speed;
    public IWeapon Weapon => _weapon;
    public Transform Transform => transform;
    public string ResourceName { get; set; }
    [Inject] private UpdateSender _updateSender;


    public void Setup(IWeapon weapon)
    {
        _weapon = weapon;
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
        }
    }

    public void Death()
    {
        _health = 0;
        _updateSender.OnUpdate -= OnUpdate;
        Events.EventBus.RaiseEvent<IDeathHandler>(h => h.HandleDeath(this));
        Uninit();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Move(Vector2 direction)
    {
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
    }
}