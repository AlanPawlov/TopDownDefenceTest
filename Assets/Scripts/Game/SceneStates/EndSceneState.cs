using Common.Events;
using Common.Events.Handlers;
using Services;

namespace States.SceneStates
{
    public class EndSceneState : BaseSceneState, IRestartMatchHandler
    {
        private readonly PlayerSpawner _playerSpawner;
        private readonly EnemySpawner _enemySpawner;

        public EndSceneState(IGameStateMachine stateMachine, PlayerSpawner playerSpawner,
            EnemySpawner enemySpawner) : base(stateMachine)
        {
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
            EventBus.Subscribe(this);
        }

        public override void EnterState()
        {
            _playerSpawner.Kill();
            _enemySpawner.KillAll();
        }

        public override void ExitState()
        {
        }

        public void HandleRestart()
        {
            StateMachine.StartState<InitSceneState>();
        }
    }
}