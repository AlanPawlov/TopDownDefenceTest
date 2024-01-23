using System.Threading.Tasks;
using CommonTemplate.Events;
using CommonTemplate.Events.Handlers;
using CommonTemplate.States.GameStates;
using CommonTemplate.UITemplate;
using CommonTemplate.UITemplate.DefaultWidgets;
using UnityEngine;
using Zenject;

namespace Game.UI.Windows
{
    public class EndMatchWindow : InterfaceWindow
    {
        [SerializeField] private Transform _headerTextContainer;
        [SerializeField] private Transform _restartButtonContainer;
        [SerializeField] private Transform _toMenuButtonContainer;
        private LabelWidget _headerText;
        private ButtonWidget _restartButton;
        private ButtonWidget _toMenuButton;
        private ProjectStateMachine _projectStateMachine;

        [Inject]
        public void Construct(ProjectStateMachine projectStateMachine)
        {
            _projectStateMachine = projectStateMachine;
        }

        public override async Task Init()
        {
            base.Init();
            _restartButton =
                await CreateChild<ButtonWidget>(UIResourceMap.WidgetMap.DefaultButton, _restartButtonContainer);
            _toMenuButton =
                await CreateChild<ButtonWidget>(UIResourceMap.WidgetMap.DefaultButton, _toMenuButtonContainer);

            _restartButton.SetData("Restart");
            _restartButton.SetData(OnRestartButtonClick);
            _toMenuButton.SetData("To Menu");
            _toMenuButton.SetData(OnMenuButtonClick);

            _headerText =
                await CreateChild<LabelWidget>(UIResourceMap.WidgetMap.DefaultLabel, _headerTextContainer);
        }

        private void OnMenuButtonClick()
        {
            Close();
            _projectStateMachine.StartState<LoadMenuState, string>("MainMenu");
        }

        private void OnRestartButtonClick()
        {
            EventBus.RaiseEvent<IRestartMatchHandler>(h => h.HandleRestart());
            Close();
        }

        public void SetData(bool isWin)
        {
            var headderText = isWin ? "Win" : "Defeat";
            _headerText.SetData(headderText);
        }

        public override void Uninit()
        {
            _restartButton = null;
            _toMenuButton = null;
            _headerText = null;
            base.Uninit();
        }
    }
}