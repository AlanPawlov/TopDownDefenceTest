using Interfaces;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable, IMovable
{
    [SerializeField] private Rigidbody2D _rigidbody;
    private int _health;
    private float _speed = 2.5f;
    private bool _canWalk;
    public int Health => _health;
    public bool CanWalk => _canWalk;
    public float Speed => _speed;

    public void ApplyDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void Death()
    {
        throw new System.NotImplementedException();
    }

    public void Move(Vector2 direction)
    {
        _rigidbody.velocity = direction * _speed;
    }
}