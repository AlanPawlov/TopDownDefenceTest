using System.Threading.Tasks;
using Common.Events;
using Common.Events.Handlers;
using Common.UITemplate;
using Common.UITemplate.DefaultWidgets;
using UnityEngine;

namespace Game.UI.Windows
{
    public class EndMatchWindow : InterfaceWindow
    {
        [SerializeField] private Transform _headerTextContainer;
        [SerializeField] private Transform _restartButtonContainer;
        private LabelWidget _headerText;
        private ButtonWidget _restartButton;

        public override async Task Init()
        {
            base.Init();
            _restartButton =
                await CreateChild<ButtonWidget>(UIResourceMap.WidgetMap.DefaultButton, _restartButtonContainer);
            _restartButton.SetData("Restart");
            _restartButton.SetData(OnRestartButtonClick);

            _headerText =
                await CreateChild<LabelWidget>(UIResourceMap.WidgetMap.DefaultLabel, _headerTextContainer);
        }

        private void OnRestartButtonClick()
        {
            EventBus.RaiseEvent<IRestartMatchHandler>(h=>h.HandleRestart());
            Uninit();
        }

        public void SetData(bool isWin)
        {
            var headderText = isWin ? "Win" : "Defeat";
            _headerText.SetData(headderText);
        }

        public override void Uninit()
        {
            _restartButton = null;
            _headerText = null;
            base.Uninit();
        }
    }
}