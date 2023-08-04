using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private int NextSceneIndex = 1;
        [SerializeField] private MainCanvas _globalCanvas;

        public override void InstallBindings()
        {
            InstallMainCanvas();
            InstallUIManager();
            InstallUIPool();
            InstallUIFactory();
            SceneManager.LoadScene(NextSceneIndex);
        }

        private void InstallMainCanvas()
        {
            Container.Bind<MainCanvas>()
                .FromComponentInNewPrefab(_globalCanvas)
                .AsSingle();
        }

        private void InstallUIManager()
        {
            Container
                .Bind<UIManager>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void InstallUIPool()
        {
            Container
                .Bind<UIElementPool>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void InstallUIFactory()
        {
            Container
                .Bind<UIFactory>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}