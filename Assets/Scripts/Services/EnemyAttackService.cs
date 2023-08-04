using System.Collections.Generic;
using Events.Handlers;
using Interfaces;
using UnityEngine;

namespace Services
{
    public class EnemyAttackService : IDeathHandler, ISpawnCharacterHandler
    {
        private UpdateSender _updateSender;
        private List<IAttackable> _enemies;
        private IDamageable _target;
        private bool _isWork;

        public EnemyAttackService(UpdateSender updateSender, Wall wall)
        {
            _updateSender = updateSender;
            _isWork = true;
            _target = wall;
            _enemies = new List<IAttackable>();
            Events.EventBus.Subscribe(this);
            _updateSender.OnUpdate += OnUpdate;
        }

        private void OnUpdate()
        {
            if (!_isWork)
                return;
            
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i] == null)
                    continue;
                var character = (Character)_enemies[i];
                var target = (Wall)_target;
                if (Mathf.Abs(target.transform.position.y - character.GetPosition().y) <=
                    _enemies[i].Weapon.AttackDistance)
                    _enemies[i].Attack(_target);
            }
        }


        private void Register(IAttackable movable)
        {
            _enemies.Add(movable);
        }

        public void Unregister(IAttackable movable)
        {
            _enemies.Remove(movable);
        }

        public void HandleDeath(Character damageable)
        {
            if (_enemies.Contains(damageable))
                Unregister(damageable);
        }

        public void HandleSpawnEnemy(Character character)
        {
            if (!_enemies.Contains(character))
                Register(character);
        }

        public void HandleSpawnPlayer(Character character)
        {
        }
    }
}