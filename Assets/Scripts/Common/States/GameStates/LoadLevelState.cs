using Common.Resource;

namespace Common.States.GameStates
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly ProjectStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(ProjectStateMachine projectStateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = projectStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void StartState(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLevelLoaded);
        }

        public void EnterState()
        {
        }

        public void ExitState()
        {
        }

        private void OnLevelLoaded()
        {
        }
    }
}