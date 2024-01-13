using Common;
using Common.Data;
using Common.GameSetting;
using Common.Resource;
using Common.Services;
using Common.States;
using Common.States.GameStates;
using Common.UITemplate;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private UpdateSender _updateSender;
        [SerializeField] private MainCanvas _globalCanvas;
        [SerializeField] private ProjectBootstrap _projectBootstrap;
        [SerializeField] private GlobalSettings _globalSettings;
        private GameData _gameData;

        public override void InstallBindings()
        {
            RegisterGameData();
            RegisterUpdateSender();
            RegisterMainCanvas();
            RegisterProjectStateMachine();
            RegisterSceneLoader();
            RegisterProgressService();
            RegisterInitializable();
            RegisterResourceLoader();
            RegisterUIManager();
            RegisterUIPool();
            RegisterUIFactory();
            RegisterStates();
            ResgisterStatesFactory();
            RegisterGameSetting();
            RegisterGlobalSetting();
        }

        private void RegisterGlobalSetting()
        {
            Container.Bind<GlobalSettings>()
                .FromInstance(_globalSettings)
                .AsSingle()
                .NonLazy();
        }


        private void RegisterGameSetting()
        {
            Container.Bind<GameSetting>()
                .FromNew()
                .AsSingle();
        }

        private void RegisterInitializable()
        {
            Container
                .BindInterfacesTo<ProjectBootstrap>()
                .FromInstance(_projectBootstrap)
                .AsSingle();
        }

        private void RegisterGameData()
        {
            Container
                .Bind<GameData>()
                .FromNew()
                .AsSingle();
        }

        private void RegisterUpdateSender()
        {
            Container
                .Bind<UpdateSender>()
                .FromComponentInNewPrefab(_updateSender)
                .AsSingle();
        }

        private void RegisterMainCanvas()
        {
            Container.Bind<MainCanvas>()
                .FromComponentInNewPrefab(_globalCanvas)
                .AsSingle();
        }

        private void RegisterProjectStateMachine()
        {
            Container
                .BindInterfacesAndSelfTo<ProjectStateMachine>()
                .AsSingle();
        }

        private void RegisterProgressService()
        {
            Container
                .BindInterfacesTo<ProgressService>()
                .FromNew()
                .AsSingle();
        }

        private void RegisterSceneLoader()
        {
            Container
                .Bind<SceneLoader>()
                .FromNew()
                .AsSingle();
        }

        private void RegisterResourceLoader()
        {
            Container
                .Bind<IResourceLoader>()
                .To<AddressablesLoader>()
                .AsSingle();
        }

        private void RegisterUIManager()
        {
            Container
                .Bind<UIManager>()
                .FromNew()
                .AsSingle();
        }

        private void RegisterUIPool()
        {
            Container
                .Bind<UIElementPool>()
                .FromNew()
                .AsSingle();
        }

        private void RegisterUIFactory()
        {
            Container
                .Bind<UIFactory>()
                .FromNew()
                .AsSingle();
        }

        private void ResgisterStatesFactory()
        {
            Container
                .Bind<StateFactory>()
                .AsSingle();
        }

        //тут non lazy для того чтобы в фабрике стейтов всё было создано
        private void RegisterStates()
        {
            Container
                .Bind<BootstrapState>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<GameState>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<LoadProgressState>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<MenuState>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<LoadLevelState>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<LoadMenuState>()
                .AsSingle()
                .NonLazy();
        }
    }
}