using Events;
using Events.Handlers;
using Services;

namespace States.MatchStates
{
    public class EndMatchState : BaseMatchState, IRestartMatchHandler
    {
        private readonly PlayerSpawner _playerSpawner;
        private readonly EnemySpawner _enemySpawner;

        public EndMatchState(IGameStateMachine stateMachine, PlayerSpawner playerSpawner,
            EnemySpawner enemySpawner) :
            base(stateMachine)
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
            _stateMachine.StartState<InitMatchState>();
        }
    }
}