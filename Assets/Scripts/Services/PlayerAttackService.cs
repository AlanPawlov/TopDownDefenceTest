using System;
using System.Collections.Generic;
using Events.Handlers;
using Interfaces;
using UnityEngine;

namespace Services
{
    public class PlayerAttackService : IDeathHandler, ISpawnCharacterHandler,IDisposable
    {
        private UpdateSender _updateSender;
        private List<IDamageable> _enemies;
        private Character.Character _player;
        private bool _isWork;

        public PlayerAttackService(UpdateSender updateSender)
        {
            _updateSender = updateSender;
            _enemies = new List<IDamageable>();
            Events.EventBus.Subscribe(this);
            _updateSender.OnUpdate += OnUpdate;
            _isWork = true;
        }

        private void Register(IDamageable movable)
        {
            _enemies.Add(movable);
        }

        public void Unregister(IDamageable movable)
        {
            _enemies.Remove(movable);
        }

        private void OnUpdate()
        {
            if (!_isWork)
                return;

            var minDistance = float.MaxValue;
            IDamageable target = null;
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i] == null)
                    continue;
                var distance = Vector3.Distance(_enemies[i].GetPosition(), _player.transform.position);
                if (distance <= _player.Weapon.AttackDistance && distance < minDistance)
                {
                    target = _enemies[i];
                    minDistance = distance;
                }
            }

            if (target == null)
                return;

            _player.Attack(target);
        }

        public void HandleDeath(Character.Character damageable)
        {
            if (_enemies.Contains(damageable))
                Unregister(damageable);
        }

        public void HandleSpawnEnemy(Character.Character character)
        {
            if (!_enemies.Contains(character))
            {
                Register(character);
            }
        }

        public void HandleSpawnPlayer(Character.Character character)
        {
            _player = character;
        }

        public void Dispose()
        {
            _updateSender.OnUpdate -= OnUpdate;
            Events.EventBus.Unsubscribe(this);
            _enemies.Clear();
        }
    }
}