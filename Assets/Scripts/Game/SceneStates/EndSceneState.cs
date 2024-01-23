using CommonTemplate.Events;
using CommonTemplate.Events.Handlers;
using CommonTemplate.States;
using Game.Character;
using Game.Environment;

namespace Game.SceneStates
{
    public class EndSceneState : BaseSceneState,IRestartMatchHandler
    {
        private readonly PlayerSpawner _playerSpawner;
        private readonly EnemySpawner _enemySpawner;
        private readonly EnemyAttackService _enemyAttackService;

        public EndSceneState(IGameStateMachine stateMachine, PlayerSpawner playerSpawner,
            EnemySpawner enemySpawner,EnemyAttackService enemyAttackService) : base(stateMachine)
        {
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
            _enemyAttackService = enemyAttackService;
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

        public override void Dispose()
        {
            base.Dispose();
            EventBus.Unsubscribe(this);
            _playerSpawner.Dispose();
            _enemySpawner.Dispose();
            _enemyAttackService.Dispose();
        }
    }
}