using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets
{
    public class TimerWidget : BaseUIElement
    {
        [SerializeField] private TMP_Text Text => _text;
        [SerializeField] private Image BackGroundImage => _backGroundImage;

        private TMP_Text _text;
        private Image _backGroundImage;

        public void SetData(int value, string format, Sprite background)
        {
            _text.text = IntToTime(value, format);
            _backGroundImage.sprite = background;
        }

        private string IntToTime(int value, string format)
        {
            throw new NotImplementedException();
        }
    }
}