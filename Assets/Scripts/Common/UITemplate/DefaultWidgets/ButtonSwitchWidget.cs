using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Common.UITemplate.DefaultWidgets
{
    public class ButtonSwitchWidget : BaseUIElement
    {
        [SerializeField] private Transform _positiveButtonContainer;
        [SerializeField] private Transform _negativeButtonContainer;
        [SerializeField] private TMP_Text Text;
        private ButtonWidget _positiveButton;
        private ButtonWidget _negativeButton;

        public override async Task Init()
        {
            await base.Init();
            _positiveButton =
                await CreateChild<ButtonWidget>(UIResourceMap.WidgetMap.ArrowButton, _positiveButtonContainer);
            _negativeButton =
                await CreateChild<ButtonWidget>(UIResourceMap.WidgetMap.ArrowButton, _negativeButtonContainer);
        }
        
        public void SetData(Action<int> positiveAction, Action<int> negaticeAction, string text = "")
        {
            _positiveButton.SetData(() => positiveAction?.Invoke(1));
            _negativeButton.SetData(() => negaticeAction?.Invoke(-1));
            Text.text = text;
        }

        public void UpdateText(string newText)
        {
            Text.text = newText;
        }

        public override void Uninit()
        {
            Text.text = string.Empty;
            _positiveButton = null;
            _negativeButton = null;
            base.Uninit();
        }
    }
}