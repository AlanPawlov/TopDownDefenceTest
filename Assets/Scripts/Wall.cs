using Interfaces;
using UnityEngine;

public class Wall : MonoBehaviour, IDamageable
{
    private int _health = 5;
    public int Health => _health;

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
        Debug.Log("END GAME");
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}