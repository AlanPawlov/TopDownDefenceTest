using Common.Resource;
using Services;

namespace States.GameStates
{
    public class LoadMenuState : IPayloadedState<string>
    {
        private readonly ProjectStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadMenuState(ProjectStateMachine projectStateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = projectStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void StartState(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnSceneLoaded).Forget();
        }

        public void EnterState()
        {
        }

        public void ExitState()
        {
        }

        private void OnSceneLoaded()
        {
            _stateMachine.StartState<MenuState>();
        }
    }
}