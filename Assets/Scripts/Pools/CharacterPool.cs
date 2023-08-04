using Events;
using Events.Handlers;
using UI;
using UnityEngine;
using Zenject;
using IPoolable = UI.IPoolable;

namespace Pools
{
    public class CharacterPool : BasePool, IDeathHandler
    {
        public CharacterPool(DiContainer container) : base(container)
        {
            EventBus.Subscribe(this);
        }

        public override IPoolable LoadFromPool<T>(string resourceName, Transform parent = null)
        {
            var obj = base.LoadFromPool<T>(resourceName, parent);
            return obj;
        }

        public Character LoadFromPool<T>(string resourceName, Vector3 transformPosition, Quaternion identity)
        {
            var obj = (Character)LoadFromPool<T>(resourceName, null);
            if (obj != null)
            {
                obj.transform.position = transformPosition;
                obj.transform.rotation = identity;
            }

            return obj;
        }

        public void HandleDeath(Character character)
        {
            RemoveToPool(character);
        }
    }
}