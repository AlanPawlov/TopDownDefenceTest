using UnityEngine;

namespace Game.Input
{
    public abstract class InputService : IInputService
    {
        public abstract Vector2 Movement { get;}
        public abstract void Init();
    }
}