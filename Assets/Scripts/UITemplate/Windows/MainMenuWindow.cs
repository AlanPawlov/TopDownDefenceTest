using System.Threading.Tasks;
using States.GameStates;
using UI;
using UI.Widgets;
using UnityEngine;
using Zenject;

namespace UITemplate.Windows
{
    public class MainMenuWindow : InterfaceWindow
    {
        [SerializeField] private Transform _headerTextContainer;
        [SerializeField] private Transform _playButtonContainer;
        private LabelWidget _headerText;
        private ButtonWidget _playButton;
        private ProjectStateMachine _stateMachine;
        private string GameplayScene = "WallDefence";

        [Inject]
        public void Construct(ProjectStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public override async Task Init()
        {
            base.Init();
            _playButton =
                await CreateChild<ButtonWidget>(UIResourceMap.WidgetMap.DefaultButtonWidget, _playButtonContainer);
            _playButton.SetData("Play");
            _playButton.SetData(OnPlayButtonClick);
            _headerText =
                await CreateChild<LabelWidget>(UIResourceMap.WidgetMap.DefaultLabelWidget, _headerTextContainer);
        }

        private void OnPlayButtonClick()
        {
            _stateMachine.StartState<LoadLevelState, string>(GameplayScene);
            Uninit();
        }
        
        public override void Uninit()
        {
            _playButton = null;
            _headerText = null;
            base.Uninit();
        }
    }
}