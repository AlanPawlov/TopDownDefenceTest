using System.Threading;
using System.Threading.Tasks;

namespace Services.MatchStates
{
    public class PrepareMatchState : BaseState
    {
        private readonly PlayerSpawner _playerSpawner;
        private CancellationToken _token;
        private CancellationTokenSource _tokenSource;

        public PrepareMatchState(IStateSwitcher stateSwitcher, PlayerSpawner playerSpawner) : base(stateSwitcher)
        {
            _playerSpawner = playerSpawner;
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
        }

        public override async void StartState()
        {
            await _playerSpawner.Spawn();
            await Task.Delay(3000, _token);
            _stateSwitcher.SwitchState<PlayMatchState>();
        }

        public override void StopState()
        {
        }

        public override void Dispose()
        {
            base.Dispose();
            _tokenSource.Cancel();
        }
    }
}