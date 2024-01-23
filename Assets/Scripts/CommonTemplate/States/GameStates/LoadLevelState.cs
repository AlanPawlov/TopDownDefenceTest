using CommonTemplate.Resource;
using CommonTemplate.UITemplate;
using CommonTemplate.UITemplate.DefaultWindows;

namespace CommonTemplate.States.GameStates
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly ProjectStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IResourceLoader _resourceLoader;
        private readonly UIManager _uiManager;
        private LoadingWindow _loadingWindow;

        public LoadLevelState(ProjectStateMachine projectStateMachine, SceneLoader sceneLoader,
            IResourceLoader resourceLoader, UIManager uiManager)
        {
            _stateMachine = projectStateMachine;
            _resourceLoader = resourceLoader;
            _sceneLoader = sceneLoader;
            _uiManager = uiManager;
        }

        public async void EnterState(string sceneName)
        {
            _resourceLoader.Release();
            _loadingWindow = await _uiManager.CreateGlobalWindow<LoadingWindow>(UIResourceMap.WindowMap.LoadingWindow);
            _sceneLoader.Load(sceneName, OnSceneLoaded, OnUpdateProgress).Forget();
        }

        private void OnUpdateProgress(float progress)
        {
            _loadingWindow.SetData("Load game...", progress);
        }

        public void EnterState()
        {
        }

        public void ExitState()
        {
            _loadingWindow.Close();
            _loadingWindow = null;
        }

        private void OnSceneLoaded()
        {
            _stateMachine.StartState<GameState>();
        }
    }
}