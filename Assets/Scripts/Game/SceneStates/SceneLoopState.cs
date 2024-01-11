using System.Linq;
using Common.Data;
using Common.Events;
using Common.Events.Handlers;
using Common.States;
using Common.UITemplate;
using Game.Character;
using Game.Environment;
using Game.UI.Windows;
using UnityEngine;

namespace Game.SceneStates
{
    public class SceneLoopState : BaseSceneState, IKillCharacterHandler, IWallDestroyedHandler
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

        public SceneLoopState(IGameStateMachine stateMachine, EnemySpawner enemySpawner, UIManager uiManager,
            PlayerMovementService playerMovementService, GameData gameData) : base(
            stateMachine)
        {
            _uiManager = uiManager;
            _playerMovementService = playerMovementService;
            _enemySpawner = enemySpawner;
            var setting = gameData.WallDefenceRules.First().Value;
            _maxKill = setting.MaxKillToWin;
            _minKill = setting.MinKillToWin;
            _walllHealth = gameData.Walls[setting.WallId].Health;
        }

        public override async void EnterState()
        {
            _targetKill = Random.Range(_minKill, _maxKill + 1);
            _hudWindow =
                await _uiManager.CreateWindow<HUDWindow>(UIResourceMap.WindowMap.HUD, WindowBehavior.Exclusive);
            _hudWindow.SetData(_targetKill, _walllHealth);
            EventBus.Subscribe(this);
            _playerMovementService.StartWork();
            _enemySpawner.StartWork();
        }

        public override void ExitState()
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
            StateMachine.StartState<EndSceneState>();
        }
    }
}