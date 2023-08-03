using Interfaces;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable, IMovable
{
    private int _health;
    private float _speed;
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
        throw new System.NotImplementedException();
    }
}