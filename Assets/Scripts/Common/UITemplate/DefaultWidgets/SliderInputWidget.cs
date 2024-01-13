using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common.UITemplate.DefaultWidgets
{
    public class SliderInputWidget : BaseUIElement
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private float _upperLimit = 100f;
        [SerializeField] private float _lowerLimit = 0;
        private string _format;

        public float Value => _slider.value;

        public override async Task Init()
        {
            await base.Init();
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
            _inputField.onEndEdit.AddListener(OnInputFieldValueChanged);
            _inputField.contentType = TMP_InputField.ContentType.DecimalNumber;
        }

        public void Clamp(float min, float max)
        {
            _lowerLimit = min;
            _upperLimit = max;
            _slider.minValue = _lowerLimit;
            _slider.maxValue = _upperLimit;
        }

        public void SetValue(float value, string format = "F1")
        {
            _format = format;
            _slider.minValue = _lowerLimit;
            _slider.maxValue = _upperLimit;

            value = Mathf.Clamp(value, _lowerLimit, _upperLimit);
            _slider.value = value;
            _inputField.text = value.ToString(format);
        }

        private void OnInputFieldValueChanged(string text)
        {
            var value = (float)System.Convert.ToDouble(_inputField.text);
            value = Mathf.Clamp(value, _lowerLimit, _upperLimit);

            _slider.value = value;
        }

        private void OnSliderValueChanged(float value)
        {
            _inputField.text = _slider.value.ToString(_format);
        }

        public void ChangeMode(bool isInteger)
        {
            _inputField.contentType = isInteger
                ? TMP_InputField.ContentType.IntegerNumber
                : TMP_InputField.ContentType.DecimalNumber;
            _slider.wholeNumbers = isInteger;
        }

        public override void Uninit()
        {
            ChangeMode(false);
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
            _inputField.onEndEdit.RemoveListener(OnInputFieldValueChanged);
            _slider.value = _lowerLimit;
            _inputField.text = _slider.value.ToString();
            Clamp(0, 1);
            base.Uninit();
        }
    }
}