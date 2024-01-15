using UnityEngine;

namespace Game.Input
{
    public interface IInputService
    {
        Vector2 Movement { get; }
        void Init();
    }
}