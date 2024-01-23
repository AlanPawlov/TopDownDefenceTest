using UnityEngine;
using UnityEngine.Audio;

namespace CommonTemplate.GameSetting
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

        public void UpdateVolume()
        {
            _audioMixer.SetFloat(KEY_MASTER_VOLUME, DenormalizeVolume(MasterVolume));
            _audioMixer.SetFloat(KEY_MUSIC_VOLUME, DenormalizeVolume(MusicVolume));
            _audioMixer.SetFloat(KEY_EFFECTS_VOLUME, DenormalizeVolume(EffectsVolume));
        }

        private float DenormalizeVolume(float volume) => Mathf.Log10(volume) * 20;

        public void SaveChanges()
        {
            _options.SaveChanges();
        }
    }
}