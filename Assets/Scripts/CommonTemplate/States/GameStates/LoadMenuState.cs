using System.Threading.Tasks;
using CommonTemplate.Resource;
using CommonTemplate.UITemplate;
using CommonTemplate.UITemplate.DefaultWindows;

namespace CommonTemplate.States.GameStates
{
    public class LoadMenuState : IPayloadedState<string>
    {
        private readonly ProjectStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IResourceLoader _resourceLoader;
        private readonly UIManager _uiManager;
        private LoadingWindow _loadingWindow;

        public LoadMenuState(ProjectStateMachine projectStateMachine, SceneLoader sceneLoader,
            IResourceLoader resourceLoader, UIManager uiManager)
        {
            _stateMachine = projectStateMachine;
            _sceneLoader = sceneLoader;
            _resourceLoader = resourceLoader;
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
            _loadingWindow.SetData("Load menu...", progress);
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
            _stateMachine.StartState<MenuState>();
        }
    }
}