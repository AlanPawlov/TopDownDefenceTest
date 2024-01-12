using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Installers
{
    public class AudioInstaller : MonoInstaller
    {
        [SerializeField] private AudioMixer _audioMixer;

        public override void InstallBindings()
        {
            RegisterAudioMixer();
        }
        
        private void RegisterAudioMixer()
        {
            Container.Bind<AudioMixer>()
                .FromInstance(_audioMixer)
                .AsSingle()
                .NonLazy();
        }
    }
}