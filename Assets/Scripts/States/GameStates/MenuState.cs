using UI;
using UI.Windows;
using UITemplate.Windows;

namespace States.GameStates
{
    public class MenuState : IState
    {
        private readonly UIManager _uiManager;

        public MenuState(ProjectStateMachine stateMachine, UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void StopState()
        {
        }

        public void StartState()
        {
            _uiManager.CreateWindow<MainMenuWindow>(UIResourceMap.WindowMap.MainMenu, WindowBehavior.Exclusive);
        }
    }
}