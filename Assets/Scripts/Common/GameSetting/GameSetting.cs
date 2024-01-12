using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace Common.GameSetting
{
    public class GameSetting
    {
        public const string KEY_MASTER_VOLUME = "Master";
        public const string KEY_MUSIC_VOLUME = "MusicVolume";
        public const string KEY_EFFECTS_VOLUME = "EffectsVolume";
        public const string KEY_LANGUAGE = "Language";
        private readonly GlobalSettings _settings;
        private AudioMixer _audioMixer;
        private Options _options = new Options();

        public Options Options => _options;

        public float MasterVolume
        {
            get { return _options.GetFloat(KEY_MASTER_VOLUME); }
            set { _options.Set(KEY_MASTER_VOLUME, value, false); }
        }

        public float MusicVolume
        {
            get { return _options.GetFloat(KEY_MUSIC_VOLUME); }
            set { _options.Set(KEY_MUSIC_VOLUME, value, false); }
        }

        public float EffectsVolume
        {
            get { return _options.GetFloat(KEY_EFFECTS_VOLUME); }
            set { _options.Set(KEY_EFFECTS_VOLUME, value, false); }
        }

        public string Language
        {
            get { return _options.GetString(KEY_LANGUAGE); }
            set { _options.Set(KEY_LANGUAGE, value, false); }
        }

        public GameSetting(GlobalSettings settings, AudioMixer audioMixer)
        {
            _audioMixer = audioMixer;
            _settings = settings;
        }

        public void Init()
        {
            var options = _settings.DefaultOptions.GetCopy();
            var prefix = $"{Application.companyName}.{Application.productName}.GameSetting";
            _options.Initialize(options, true, prefix);
            UpdateVolume();
            SaveChanges();
        }

        public async void UpdateVolume()
        {
            //TODO: на этапе инжекции аудиомиксер не успевает применить значения
            //для решения нужно применять настройки в бутстрап стейте, через Init, а не через конструктор и убрать это ожидание
            await Task.Delay(1000);
            _audioMixer.SetFloat(KEY_MASTER_VOLUME, Mathf.Log10(MasterVolume) * 20);
            _audioMixer.SetFloat(KEY_MUSIC_VOLUME, Mathf.Log10(MusicVolume) * 20);
            _audioMixer.SetFloat(KEY_EFFECTS_VOLUME, Mathf.Log10(EffectsVolume) * 20);
        }

        public void SaveChanges()
        {
            _options.SaveChanges();
        }
    }
}