using Common.Services;

namespace Common.States.GameStates
{
    public class LoadProgressState : IState
    {
        private string _menuScene = "MainMenu";
        private readonly ProjectStateMachine _projectStateMachine;
        private readonly IProgressService _progressService;

        public LoadProgressState(ProjectStateMachine projectStateMachine, IProgressService progressService)
        {
            _projectStateMachine = projectStateMachine;
            _progressService = progressService;
        }

        public void EnterState()
        {
            LoadProgress();
        }

        public void ExitState()
        {
        }

        private void LoadProgress()
        {
            _progressService.Progress = CreateNewProgressData();
            _projectStateMachine.StartState<LoadMenuState, string>(_menuScene);
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