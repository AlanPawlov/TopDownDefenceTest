using System.Threading;
using System.Threading.Tasks;
using CommonTemplate.States;
using Game.Environment;

namespace Game.SceneStates
{
    public class InitSceneState : BaseSceneState
    {
        private readonly PlayerSpawner _playerSpawner;
        private CancellationToken _token;
        private CancellationTokenSource _tokenSource;
        private readonly LevelBorderHelper _levelBorderHelper;

        public InitSceneState(IGameStateMachine stateMachine, PlayerSpawner playerSpawner,
            LevelBorderHelper levelBorderHelper) : base(stateMachine)
        {
            _levelBorderHelper = levelBorderHelper;
            _playerSpawner = playerSpawner;
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
        }

        public override async void EnterState()
        {
            await _levelBorderHelper.SetupBorders();
            await _playerSpawner.Spawn();
            await Task.Delay(3000, _token);
            StateMachine.StartState<SceneLoopState>();
        }

        public override void ExitState()
        {
        }

        public override void Dispose()
        {
            base.Dispose();
            _tokenSource.Cancel();
            _tokenSource = null;
            _playerSpawner.Dispose();
            _levelBorderHelper.Dispose();
        }
    }
}