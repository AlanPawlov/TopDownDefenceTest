using System.Threading;
using System.Threading.Tasks;
using Resource;
using Services;
using UnityEngine;

namespace States.MatchStates
{
    public class InitMatchState : BaseMatchState
    {
        private readonly PlayerSpawner _playerSpawner;
        private CancellationToken _token;
        private CancellationTokenSource _tokenSource;
        private readonly IResourceLoader _resourceLoader;

        public InitMatchState(IGameStateMachine stateMachine, PlayerSpawner playerSpawner,
            IResourceLoader resourceLoader) : base(stateMachine)
        {
            _resourceLoader = resourceLoader;
            _playerSpawner = playerSpawner;
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
        }

        public override async void StartState()
        {
            SetupCameraBorder();
            await _playerSpawner.Spawn();
            await Task.Delay(3000, _token);
            _stateMachine.StartState<LoopMatchState>();
        }

        public override void StopState()
        {
        }

        private async void SetupCameraBorder()
        {
            var camera = Camera.main;
            var leftBorderMax = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            var rightBorderMax = camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            var topBorderMax = camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
            var bottomBorderMax = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
            var horizontalBorderSize = new Vector2(rightBorderMax - leftBorderMax, 1);
            var verticalBorderSize = new Vector2(1, topBorderMax - bottomBorderMax);
            var leftBorderBorder = await _resourceLoader.Load<BoxCollider2D>("Border");
            leftBorderBorder.size = verticalBorderSize;
            leftBorderBorder.transform.position = new Vector3(leftBorderMax - leftBorderBorder.size.x / 2, 0, 0);
            var rightBorderBorder = await _resourceLoader.Load<BoxCollider2D>("Border");
            rightBorderBorder.size = verticalBorderSize;
            rightBorderBorder.transform.position = new Vector3(rightBorderMax + leftBorderBorder.size.x / 2, 0, 0);
            var topBorderBorder = await _resourceLoader.Load<BoxCollider2D>("Border");
            topBorderBorder.size = horizontalBorderSize;
            topBorderBorder.transform.position = new Vector3(0, topBorderMax + topBorderBorder.size.y / 2, 0);
            var bottomBorderBorder = await _resourceLoader.Load<BoxCollider2D>("Border");
            bottomBorderBorder.size = horizontalBorderSize;
            bottomBorderBorder.transform.position = new Vector3(0, bottomBorderMax - bottomBorderBorder.size.y / 2, 0);
        }


        public override void Dispose()
        {
            base.Dispose();
            _tokenSource.Cancel();
            _tokenSource = null;
        }
    }
}