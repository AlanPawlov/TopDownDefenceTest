using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CommonTemplate.UITemplate.DefaultWidgets
{
    public class ButtonWidget : BaseUIElement
    {
        [SerializeField] protected Button _button;
        [SerializeField] protected Image _targetImage;
        [SerializeField] protected TMP_Text _buttonText;

        public override async Task Init()
        {
            await base.Init();
            RectTransform.anchorMin = Vector2.zero;
            RectTransform.anchorMax = Vector2.one;
            RectTransform.offsetMax = Vector2.zero;
            RectTransform.offsetMin = Vector2.zero;
        }

        public override void Uninit()
        {
            if (_buttonText != null)
                _buttonText.text = default;

            _button.interactable = true;
            _button.onClick.RemoveAllListeners();
            base.Uninit();
        }


        public void SetData(Action action)
        {
            _button.interactable = true;
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => action());
        }

        public void SetData(string newText)
        {
            if (_buttonText == null)
            {
                return;
            }

            _buttonText.text = newText;
        }
    }
}