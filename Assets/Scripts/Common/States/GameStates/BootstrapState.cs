using Common.Data;
using Common.Resource;
using Services;

namespace States.GameStates
{
    public class BootstrapState : IState
    {
        private string _bootstrapScene = "Bootstrap";
        private readonly ProjectStateMachine _stateMachine;
        private readonly GameData _gameData;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(ProjectStateMachine projectStateMachine, GameData gameData, SceneLoader sceneLoader)
        {
            _stateMachine = projectStateMachine;
            _gameData = gameData;
            _sceneLoader = sceneLoader;
        }

        public void EnterState()
        {
            _sceneLoader.Load(_bootstrapScene, EnterSceneLevel).Forget();
        }

        private void EnterSceneLevel()
        {
            _gameData.Init();
            _stateMachine.StartState<LoadProgressState>();
        }

        public void ExitState()
        {
        }
    }
}