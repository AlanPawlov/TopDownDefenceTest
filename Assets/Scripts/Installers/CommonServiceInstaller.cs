using DefaultNamespace;
using Resource;
using Services;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CommonServiceInstaller : MonoInstaller
    {
        [SerializeField] private UpdateSender _updateSender;

        public override void InstallBindings()
        {
            BindResourceLoader();
            BindUpdateSender();
        }

        private void BindUpdateSender()
        {
            Container
                .Bind<UpdateSender>()
                .FromComponentInNewPrefab(_updateSender)
                .AsSingle();
        }

        private void BindResourceLoader()
        {
            Container
                .Bind<IResourceLoader>()
                .To<ResourceLoader>()
                .AsSingle();
        }
    }
}