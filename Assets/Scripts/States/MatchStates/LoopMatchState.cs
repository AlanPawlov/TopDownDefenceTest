using System.Collections.Generic;
using System.Linq;
using Events;
using Events.Handlers;
using Models;
using Services;
using UI;
using UI.Windows;
using UnityEngine;

namespace States.MatchStates
{
    public class LoopMatchState : BaseMatchState, IKillCharacterHandler, IWallDestroyedHandler
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

        public LoopMatchState(IGameStateMachine stateMachine, EnemySpawner enemySpawner, UIManager uiManager,
            Dictionary<string,WallDefenceRulesModel> rules, Dictionary<string, WallModel> wallModels,
            PlayerMovementService playerMovementService) : base(
            stateMachine)
        {
            _uiManager = uiManager;
            _playerMovementService = playerMovementService;
            _enemySpawner = enemySpawner;
            var setting = rules.First().Value;
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
            _hudWindow.Close();
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
            _stateMachine.StartState<EndMatchState>();
        }
    }
}