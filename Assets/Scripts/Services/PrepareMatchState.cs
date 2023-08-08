using System.Threading.Tasks;

namespace Services
{
    public class PrepareMatchState : BaseState
    {
        private readonly PlayerSpawner _playerSpawner;

        public PrepareMatchState(IStateSwitcher stateSwitcher, PlayerSpawner playerSpawner) : base(stateSwitcher)
        {
            _playerSpawner = playerSpawner;
        }

        public override async void StartState()
        {
            _playerSpawner.Spawn();
            await Task.Delay(3000);
            _stateSwitcher.SwitchState<PlayMatchState>();
        }

        public override void StopState()
        {
        }
    }
}