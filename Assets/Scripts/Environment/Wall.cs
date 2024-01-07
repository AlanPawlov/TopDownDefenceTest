using System.Collections.Generic;
using System.Linq;
using Events;
using Events.Handlers;
using Interfaces;
using Models;
using UnityEngine;
using Zenject;

namespace Environment
{
    public class Wall : MonoBehaviour, IDamageable, IRestartMatchHandler
    {
        private string _id;
        private int _maxHealth;
        private int _curHealth;
        public int Health => _curHealth;

        [Inject]
        public void Construct(Dictionary<string, WallModel> wallModels, Dictionary<string,WallDefenceRulesModel> setting)
        {
            _id = setting.First().Value.WallId;
            _maxHealth = wallModels[_id].Health;
            _curHealth = _maxHealth;
        }

        private void Awake()
        {
            EventBus.Subscribe(this);
        }

        public void ApplyDamage(int damage)
        {
            _curHealth -= damage;
            EventBus.RaiseEvent<IWallDamageHandler>(h => h.HandleWallDamage(_curHealth));
            if (_curHealth <= 0)
            {
                Death();
            }
        }

        public void Death()
        {
            EventBus.RaiseEvent<IWallDestroyedHandler>(h => h.HandleWallDestroyed());
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void HandleRestart()
        {
            _curHealth = _maxHealth;
        }

        private void OnDestroy()
        {
            EventBus.Unsubscribe(this);
        }
    }
}