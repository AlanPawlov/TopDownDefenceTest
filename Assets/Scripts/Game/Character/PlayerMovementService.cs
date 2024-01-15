using System;
using Common;
using Common.Events;
using Common.Events.Handlers;
using Game.Input;
using Game.Input.Mobile;
using UnityEngine;

namespace Game.Character
{
    public class PlayerMovementService : IDisposable, ISpawnCharacterHandler, IDeathHandler
    {
        private readonly IInputService _inputService;
        private UpdateSender _updateSender;
        private Character _player;
        private Vector2 _direction;

        public PlayerMovementService(UpdateSender updateSender,IInputService inputService)
        {
            _updateSender = updateSender;
            _inputService = inputService;
            EventBus.Subscribe(this);
        }

        public void StartWork()
        {
            _updateSender.OnUpdate += OnUpdate;
            _updateSender.OnFixedUpdate += OnFixedUpdate;
        }

        private void OnUpdate()
        {
            var horizontal = _inputService.Movement.x;
            var vertical = _inputService.Movement.y;
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