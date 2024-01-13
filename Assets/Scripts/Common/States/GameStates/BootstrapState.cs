using Common.Data;
using Common.Resource;

namespace Common.States.GameStates
{
    public class BootstrapState : IState
    {
        private string _bootstrapScene = "Bootstrap";
        private readonly ProjectStateMachine _stateMachine;
        private readonly GameData _gameData;
        private readonly SceneLoader _sceneLoader;
        private readonly GameSetting.GameSetting _gameSetting;

        public BootstrapState(ProjectStateMachine projectStateMachine, GameData gameData, SceneLoader sceneLoader,
            GameSetting.GameSetting gameSetting)
        {
            _stateMachine = projectStateMachine;
            _gameData = gameData;
            _sceneLoader = sceneLoader;
            _gameSetting = gameSetting;
        }

        public void EnterState()
        {
            _sceneLoader.Load(_bootstrapScene, EnterSceneLevel).Forget();
        }

        private void EnterSceneLevel()
        {
            _gameData.Init();
            _gameSetting.Init();
            _stateMachine.StartState<LoadProgressState>();
        }

        public void ExitState()
        {
        }
    }
}