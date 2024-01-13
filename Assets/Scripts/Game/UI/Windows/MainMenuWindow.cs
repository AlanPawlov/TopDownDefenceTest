using System.Threading.Tasks;
using Common.States.GameStates;
using Common.UITemplate;
using Common.UITemplate.DefaultWidgets;
using UnityEngine;
using Zenject;

namespace Game.UI.Windows
{
    public class MainMenuWindow : InterfaceWindow
    {
        [SerializeField] private Transform _headerTextContainer;
        [SerializeField] private Transform _playButtonContainer;
        [SerializeField]private Transform _settingButtonContainer;
        private LabelWidget _headerText;
        private ButtonWidget _settingButtton;
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
            await base.Init();
            _playButton =
                await CreateChild<ButtonWidget>(UIResourceMap.WidgetMap.DefaultButton, _playButtonContainer);
            _settingButtton =
                await CreateChild<ButtonWidget>(UIResourceMap.WidgetMap.DefaultButton, _settingButtonContainer);
            _headerText =
                await CreateChild<LabelWidget>(UIResourceMap.WidgetMap.DefaultLabel, _headerTextContainer);

            _playButton.SetData("Play");
            _settingButtton.SetData("Setting");
            _playButton.SetData(OnPlayButtonClick);
            _settingButtton.SetData(OnSettingButtonClick);
        }

        private async void OnSettingButtonClick()
        {
            Close();
            var setting =
                await _uiManager.CreateWindow<SettingWindow>(UIResourceMap.WindowMap.SettingWindow,
                    WindowBehavior.Exclusive);
            setting.SetData(() =>
                _uiManager.CreateWindow<MainMenuWindow>(UIResourceMap.WindowMap.MainMenuWindow,
                    WindowBehavior.Exclusive));
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