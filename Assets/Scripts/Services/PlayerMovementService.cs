using System;
using Events.Handlers;
using Interfaces;
using UnityEngine;

namespace Services
{
    public class PlayerMovementService : IDisposable, ISpawnCharacterHandler, IDeathHandler
    {
        private UpdateSender _updateSender;
        private Character _player;
        private Vector2 _direction;

        public PlayerMovementService(UpdateSender updateSender)
        {
            _updateSender = updateSender;
            _updateSender.OnUpdate += OnUpdate;
            _updateSender.OnFixedUpdate += OnFixedUpdate;
            Events.EventBus.Subscribe(this);
        }

        private void OnUpdate()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            _direction = new Vector2(horizontal, vertical).normalized;
        }

        public void Register(IMovable movable)
        {
            _player = (Character)movable;
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

        public void HandleDeath(Character damageable)
        {
            if (damageable == _player)
                Unregister(damageable);
        }

        public void HandleSpawnEnemy(Character character)
        {
        }

        public void HandleSpawnPlayer(Character character)
        {
            Register(character);
        }

        public void Dispose()
        {
            Events.EventBus.Unsubscribe(this);
            _updateSender.OnUpdate -= OnUpdate;
            _updateSender.OnFixedUpdate -= OnFixedUpdate;
            _player = null;
            _updateSender = null;
        }
    }
}