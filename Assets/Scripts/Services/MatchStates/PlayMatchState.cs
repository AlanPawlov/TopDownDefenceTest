using System.Collections.Generic;
using Events;
using Events.Handlers;
using Models;
using SO;
using UI;
using UI.Windows;
using UnityEngine;

namespace Services.MatchStates
{
    public class PlayMatchState : BaseState, IKillCharacterHandler, IWallDestroyedHandler
    {
        private readonly EnemySpawner _enemySpawner;
        private int _maxKill;
        private int _minKill;
        private int _targetKill;
        private int _curKill;
        private int _walllHealth;
        private UIManager _uiManager;
        private HUDWindow _hudWindow;
        private PlayerMovementService _playerMovementService;

        public PlayMatchState(IStateSwitcher stateSwitcher, EnemySpawner enemySpawner, UIManager uiManager,
            GameSetting setting, Dictionary<string, WallModel> wallModels,
            PlayerMovementService playerMovementService) : base(
            stateSwitcher)
        {
            _uiManager = uiManager;
            _playerMovementService = playerMovementService;
            _enemySpawner = enemySpawner;
            _maxKill = setting.MaxKillToWin;
            _minKill = setting.MinKillToWin;
            _walllHealth = wallModels[setting.WallId].Health;
        }

        public override async void StartState()
        {
            _targetKill = Random.Range(_minKill, _maxKill + 1);
            _hudWindow =
                await _uiManager.CreateWindow<HUDWindow>(UIResourceMap.WindowMap.HUD, WindowBehavior.Exclusive);
            _hudWindow.SetData(_targetKill, _walllHealth);
            EventBus.Subscribe(this);
            _playerMovementService.StartWork();
            _enemySpawner.StartWork();
        }

        public override void StopState()
        {
            EventBus.Unsubscribe(this);
            _curKill = 0;
            _hudWindow?.Close();
            _hudWindow = null;
            _enemySpawner.StopWork();
            _playerMovementService.StopWork();
        }

        public void HandleKillCharacter()
        {
            _curKill++;
            if (_curKill >= _targetKill)
                EndMatch(true);
        }

        public void HandleWallDestroyed()
        {
            EndMatch(false);
        }

        private async void EndMatch(bool isWin)
        {
            var endGameWindow =
                await _uiManager.CreateWindow<EndMatchWindow>(UIResourceMap.WindowMap.EndGame,
                    WindowBehavior.Exclusive);
            endGameWindow.SetData(isWin);
            _stateSwitcher.SwitchState<EndMatchState>();
        }
    }
}