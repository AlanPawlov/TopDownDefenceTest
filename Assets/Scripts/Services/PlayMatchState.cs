using Events;
using Events.Handlers;
using UI;
using UI.Windows;

namespace Services
{
    public class PlayMatchState : BaseState, IKillCharacterHandler, IWallDestroyedHandler
    {
        private readonly EnemySpawner _enemySpawner;
        private int _maxKill = 5;
        private int _curKill;
        private readonly UIManager _uiManager;
        private HUDWindow _hudWindow;

        public PlayMatchState(IStateSwitcher stateSwitcher, EnemySpawner enemySpawner, UIManager uiManager) : base(
            stateSwitcher)
        {
            _uiManager = uiManager;
            _enemySpawner = enemySpawner;
        }

        public override async void StartState()
        {
            EventBus.Subscribe(this);
            _hudWindow =
                await _uiManager.CreateWindow<HUDWindow>(UIResourceMap.WindowMap.HUD, WindowBehavior.Exclusive);
            _enemySpawner.StartWork();
        }

        public override void StopState()
        {
            EventBus.Unsubscribe(this);
            _curKill = 0;
            _hudWindow.Close();
            _hudWindow = null;
            _enemySpawner.StopWork();
        }

        public void HandleKillCharacter()
        {
            _curKill++;
            if (_curKill >= _maxKill)
                _stateSwitcher.SwitchState<EndMatchState>();
        }

        public void HandleWallDestroyed()
        {
            _stateSwitcher.SwitchState<EndMatchState>();
        }
    }
}