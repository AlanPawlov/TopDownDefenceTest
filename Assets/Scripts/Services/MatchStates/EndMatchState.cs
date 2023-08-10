using Events;
using Events.Handlers;

namespace Services.MatchStates
{
    public class EndMatchState : BaseState, IRestartMatchHandler
    {
        private readonly PlayerSpawner _playerSpawner;
        private readonly EnemySpawner _enemySpawner;

        public EndMatchState(IStateSwitcher stateSwitcher, PlayerSpawner playerSpawner, EnemySpawner enemySpawner,
            PlayerMovementService playerMovementService) : base(stateSwitcher)
        {
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
            EventBus.Subscribe(this);
        }

        public override void StartState()
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