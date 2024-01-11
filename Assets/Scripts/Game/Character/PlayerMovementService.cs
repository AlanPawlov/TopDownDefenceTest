using System;
using Common;
using Common.Events;
using Common.Events.Handlers;
using UnityEngine;

namespace Game.Character
{
    public class PlayerMovementService : IDisposable, ISpawnCharacterHandler, IDeathHandler
    {
        private UpdateSender _updateSender;
        private global::Game.Character.Character _player;
        private Vector2 _direction;

        public PlayerMovementService(UpdateSender updateSender)
        {
            _updateSender = updateSender;
            EventBus.Subscribe(this);
        }

        public void StartWork()
        {
            _updateSender.OnUpdate += OnUpdate;
            _updateSender.OnFixedUpdate += OnFixedUpdate;
        }

        private void OnUpdate()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            _direction = new Vector2(horizontal, vertical).normalized;
        }

        public void Register(IMovable movable)
        {
            _player = (global::Game.Character.Character)movable;
        }

        public void Unregister(IMovable movable)
        {
            _player = null;
        }

        private void OnFixedUpdate()
        {
            if (!_player)
                return;
            _player.Move(_direction);
        }

        public void HandleDeath(global::Game.Character.Character damageable)
        {
            if (damageable == _player)
                Unregister(damageable);
        }

        public void HandleSpawnEnemy(global::Game.Character.Character character)
        {
        }

        public void HandleSpawnPlayer(global::Game.Character.Character character)
        {
            Register(character);
        }

        public void StopWork()
        {
            _updateSender.OnUpdate -= OnUpdate;
            _updateSender.OnFixedUpdate -= OnFixedUpdate;
        }
        
        public void Dispose()
        {
            EventBus.Unsubscribe(this);
            _updateSender.OnUpdate -= OnUpdate;
            _updateSender.OnFixedUpdate -= OnFixedUpdate;
            _player = null;
            _updateSender = null;
        }
    }
}