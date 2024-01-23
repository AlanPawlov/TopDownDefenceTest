using System.Threading.Tasks;
using CommonTemplate.Events;
using CommonTemplate.Events.Handlers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Camera
{
    public class CameraShake : MonoBehaviour, IWallDamageHandler
    {
        private void Awake()
        {
            EventBus.Subscribe(this);
        }

        private async void Shake(float force = 0.25f, float duration = 0.2f)
        {
            var position = transform.localPosition;
            var time = 0f;
            while (time < duration)
            {
                var newX = Random.Range(-1f, 1f) * force;
                var newY = Random.Range(-1f, 1f) * force;
                var newPosition = new Vector3(newX, newY, position.z);
                transform.localPosition = newPosition;
                time += Time.deltaTime;
                await Task.Yield();
            }

            transform.localPosition = position;
        }

        public void HandleWallDamage(int health)
        {
            Shake();
        }

        private void OnDestroy()
        {
            EventBus.Unsubscribe(this);
        }
    }
}