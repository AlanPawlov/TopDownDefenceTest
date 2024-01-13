using Common.Data;
using Common.Resource;
using Common.WebRequest;

namespace Common.States.GameStates
{
    public class BootstrapState : IState
    {
        private string _bootstrapScene = "Bootstrap";
        private readonly ProjectStateMachine _stateMachine;
        private readonly GameData _gameData;
        private readonly SceneLoader _sceneLoader;
        private readonly GameSetting.GameSetting _gameSetting;
        private readonly WebRequestSender _webRequestSender;
        private readonly GameDataLoader _gameDataLoader;

        public BootstrapState(ProjectStateMachine projectStateMachine, GameData gameData,GameDataLoader gameDataLoader,
            SceneLoader sceneLoader, GameSetting.GameSetting gameSetting,WebRequestSender webRequestSender)
        {
            _stateMachine = projectStateMachine;
            _gameDataLoader = gameDataLoader;
            _gameData = gameData;
            _sceneLoader = sceneLoader;
            _gameSetting = gameSetting;
            _webRequestSender = webRequestSender;
        }

        public void EnterState()
        {
            _sceneLoader.Load(_bootstrapScene, EnterSceneLevel).Forget();
        }

        private async void EnterSceneLevel()
        {
            await _gameDataLoader.Init();
            _gameData.Init();
            _gameSetting.Init();
            _stateMachine.StartState<LoadProgressState>();
        }

        public void ExitState()
        {
        }
    }
}