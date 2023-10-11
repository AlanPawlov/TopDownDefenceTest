using Services;

namespace States.GameStates
{
    public class LoadProgressState : IState
    {
        private string BootstrapScene = "Bootstrap";
        private string MenuScene = "MainMenu";
        private readonly ProjectStateMachine _projectStateMachine;
        private readonly IProgressService _progressService;
        private readonly SceneLoader _sceneLoader;

        public LoadProgressState(ProjectStateMachine projectStateMachine, IProgressService progressService,
            SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _projectStateMachine = projectStateMachine;
            _progressService = progressService;
        }

        public void StartState()
        {
            _sceneLoader.Load(BootstrapScene, EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            LoadProgress();
            _projectStateMachine.StartState<LoadMenuState, string>(MenuScene);
        }


        public void StopState()
        {
        }

        private void LoadProgress()
        {
            _progressService.Progress = CreateNewProgressData();
        }

        private PlayerProgress CreateNewProgressData()
        {
            var progress = new PlayerProgress()
            {
                Id = "0",
                Name = "Player"
            };
            
            return progress;
        }
    }
}