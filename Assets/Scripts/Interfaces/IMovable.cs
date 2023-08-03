using UnityEngine;

namespace Interfaces
{
    public interface IMovable
    {
        public bool CanWalk { get; }
        public float Speed { get; }
        public void Move(Vector2 direction);
    }
}