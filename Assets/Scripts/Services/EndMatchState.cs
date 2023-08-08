using Events;
using Events.Handlers;
using UI;
using UI.Windows;

namespace Services
{
    public class EndMatchState : BaseState, IRestartMatchHandler
    {
        private readonly PlayerSpawner _playerSpawner;
        private readonly UIManager _uiManager;
        private readonly EnemySpawner _enemySpawner;

        public EndMatchState(IStateSwitcher stateSwitcher, PlayerSpawner playerSpawner, EnemySpawner enemySpawner,
            UIManager uiManager) : base(stateSwitcher)
        {
            _uiManager = uiManager;
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
            EventBus.Subscribe(this);
        }

        public override async void StartState()
        {
            _playerSpawner.Kill();
            _enemySpawner.KillAll();
        }

        public override void StopState()
        {
        }

        public void HandleRestart()
        {
            _stateSwitcher.SwitchState<PrepareMatchState>();
        }
    }
}