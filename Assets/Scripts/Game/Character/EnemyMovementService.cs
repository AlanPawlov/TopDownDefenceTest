using System;
using System.Collections.Generic;
using Common;
using Common.Events;
using Common.Events.Handlers;
using UnityEngine;

namespace Game.Character
{
    public class EnemyMovementService : ISpawnCharacterHandler, IDeathHandler, IDisposable
    {
        private List<IMovable> _enemies;
        private UpdateSender _updateSender;

        public EnemyMovementService(UpdateSender updateSender)
        {
            _enemies = new List<IMovable>();
            _updateSender = updateSender;
            _updateSender.OnFixedUpdate += OnFixedUpdate;
            EventBus.Subscribe(this);
        }

        private void Register(IMovable movable)
        {
            _enemies.Add(movable);
        }

        public void Unregister(IMovable movable)
        {
            _enemies.Remove(movable);
        }


        private void OnFixedUpdate()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i] == null)
                    continue;
                _enemies[i].Move(Vector2.down);
            }
        }

        public void HandleDeath(global::Game.Character.Character damageable)
        {
            if (_enemies.Contains(damageable))
                Unregister(damageable);
        }

        public void HandleSpawnEnemy(global::Game.Character.Character character)
        {
            if (!_enemies.Contains(character))
            {
                Register(character);
            }
        }

        public void HandleSpawnPlayer(global::Game.Character.Character character)
        {
        }

        public void Dispose()
        {
            EventBus.Unsubscribe(this);
            _updateSender.OnFixedUpdate -= OnFixedUpdate;
            _enemies.Clear();
            _updateSender = null;
        }
    }
}