using System;
using System.Threading.Tasks;
using Common.GameSetting;
using Common.UITemplate;
using Common.UITemplate.DefaultWidgets;
using UnityEngine;
using Zenject;

namespace Game.UI.Windows
{
    public class SettingWindow : InterfaceWindow
    {
        [SerializeField] private Transform _localizationContainer;
        [SerializeField] private Transform _applyChangesButtonContainer;
        [SerializeField] private Transform _closeButtonContainer;
        [SerializeField] private Transform _mainAudioVolumeSliderContainer;
        [SerializeField] private Transform _musicAudioVolumeSliderContainer;
        [SerializeField] private Transform _sfxAudioVolumeSliderContainer;
        private ButtonSwitchWidget _localizationWidget;
        private SliderInputWidget _mainAudioVolumeSlider;
        private SliderInputWidget _musicAudioVolumeSlider;
        private SliderInputWidget _sfxAudioVolumeSlider;
        private ButtonWidget _closeButton;
        private ButtonWidget _applyChangesButton;
        private Action _onCloseClickAction;
        private GameSetting _gameSetting;
        private int _curLanguage;

        [Inject]
        public void Construct(GameSetting setting)
        {
            _gameSetting = setting;
        }

        public override async Task Init()
        {
            await base.Init();
            _localizationWidget =
                await CreateChild<ButtonSwitchWidget>(UIResourceMap.WidgetMap.ButtonSwitchWidget,
                    _localizationContainer);
            _applyChangesButton =
                await CreateChild<ButtonWidget>(UIResourceMap.WidgetMap.DefaultButton, _applyChangesButtonContainer);
            _closeButton =
                await CreateChild<ButtonWidget>(UIResourceMap.WidgetMap.CloseButton, _closeButtonContainer);

            _applyChangesButton.SetData("Apply changes");
            _applyChangesButton.SetData(ApplyChanges);

            _mainAudioVolumeSlider = await CreateChild<SliderInputWidget>(UIResourceMap.WidgetMap.SliderInputWidget,
                _mainAudioVolumeSliderContainer);
            _mainAudioVolumeSlider.Clamp(0, 100);
            _musicAudioVolumeSlider = await CreateChild<SliderInputWidget>(UIResourceMap.WidgetMap.SliderInputWidget,
                _musicAudioVolumeSliderContainer);
            _musicAudioVolumeSlider.Clamp(0, 100);
            _sfxAudioVolumeSlider = await CreateChild<SliderInputWidget>(UIResourceMap.WidgetMap.SliderInputWidget,
                _sfxAudioVolumeSliderContainer);
            _sfxAudioVolumeSlider.Clamp(0, 100);

            _mainAudioVolumeSlider.ChangeMode(isInteger: true);
            _musicAudioVolumeSlider.ChangeMode(isInteger: true);
            _sfxAudioVolumeSlider.ChangeMode(isInteger: true);

            _localizationWidget.SetData(OnChangeLanguageClick, OnChangeLanguageClick, _gameSetting.Language);
            LoadUserPreferences();
        }

        private void OnChangeLanguageClick(int i)
        {
            var languages = new[] { "eng", "ru" };
            var count = languages.Length;
            var curLanguage = _curLanguage;

            var result = curLanguage + i;
            if (result >= count)
                result = 0;
            if (result < 0)
                result = count - 1;
            _curLanguage = result;
            _localizationWidget.UpdateText(languages[_curLanguage]);
        }

        public void SetData(Action onCloseClickAction)
        {
            _onCloseClickAction = onCloseClickAction;
            _closeButton.SetData(OnCloseButtonClick);
        }

        private void OnCloseButtonClick()
        {
            _onCloseClickAction?.Invoke();
            Close();
        }

        private void ApplyChanges()
        {
            var languages = new[] { "eng", "ru" };
            _gameSetting.MasterVolume = _mainAudioVolumeSlider.Value / 100;
            _gameSetting.MusicVolume = _musicAudioVolumeSlider.Value / 100;
            _gameSetting.EffectsVolume = _sfxAudioVolumeSlider.Value / 100;
            _gameSetting.Language = languages[_curLanguage];
            _gameSetting.UpdateVolume();
            _gameSetting.SaveChanges();
            LoadUserPreferences();
        }

        private void LoadUserPreferences()
        {
            var format = "F0";
            _mainAudioVolumeSlider.SetValue(_gameSetting.Options.GetFloat(GameSetting.KEY_MASTER_VOLUME) * 100, format);
            _musicAudioVolumeSlider.SetValue(_gameSetting.Options.GetFloat(GameSetting.KEY_MUSIC_VOLUME) * 100, format);
            _sfxAudioVolumeSlider.SetValue(_gameSetting.Options.GetFloat(GameSetting.KEY_EFFECTS_VOLUME) * 100, format);
        }

        public override void Uninit()
        {
            _mainAudioVolumeSlider = null;
            _sfxAudioVolumeSlider = null;
            _musicAudioVolumeSlider = null;
            _onCloseClickAction = null;
            _closeButton = null;
            _applyChangesButton = null;
            base.Uninit();
        }
    }
}