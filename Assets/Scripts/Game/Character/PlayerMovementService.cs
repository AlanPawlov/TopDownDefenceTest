using System;
using Common.Events;
using Common.Events.Handlers;
using Interfaces;
using UnityEngine;

namespace Services
{
    public class PlayerMovementService : IDisposable, ISpawnCharacterHandler, IDeathHandler
    {
        private UpdateSender _updateSender;
        private Character.Character _player;
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
            _player = (Character.Character)movable;
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

        public void HandleDeath(Character.Character damageable)
        {
            if (damageable == _player)
                Unregister(damageable);
        }

        public void HandleSpawnEnemy(Character.Character character)
        {
        }

        public void HandleSpawnPlayer(Character.Character character)
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