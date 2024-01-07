using System.Collections.Generic;
using Data;
using Models;
using Resource;
using Services;
using SO;
using States.GameStates;
using UI;
using UITemplate;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CommonServiceInstaller : MonoInstaller
    {
        [SerializeField] private UpdateSender _updateSender;
        [SerializeField] private MainCanvas _globalCanvas;
        [SerializeField] private GameSetting _setting;
        [SerializeField] private ProjectBootstrap _projectBootstrap;
        private GameData _gameData;
        
        public override void InstallBindings()
        {
            InitGamedata();
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

            //всё что ниже инжектится с сошек потом будет в бутстрап стейте с джсонов грузиться
            RegisterWeaponModels();
            RegisterCharacterModels();
            RegisterEnemySpawners();
            RegisterWallModels();
            RegisterProjectileModels();
            RegisterSetting();
        }

        private void InitGamedata()
        {
            _gameData = new GameData();
            _gameData.Init();
        }

        private void RegisterInitializable()
        {
            Container
                .BindInterfacesTo<ProjectBootstrap>()
                .FromInstance(_projectBootstrap)
                .AsSingle();
        }

        private void RegisterProjectileModels()
        {
            var projectiles = _gameData.Projectiles;
            Container
                .Bind<Dictionary<string, ProjectileModel>>()
                .FromInstance(projectiles)
                .AsSingle();
        }

        private void RegisterWallModels()
        {
            var walls = _gameData.Walls;
            Container
                .Bind<Dictionary<string, WallModel>>()
                .FromInstance((walls))
                .AsSingle();
        }

        private void RegisterEnemySpawners()
        {
            var spawners = _gameData.EnemySpawners;
            Container
                .Bind<Dictionary<string, EnemySpawnerModel>>()
                .FromInstance(spawners)
                .AsSingle();
        }

        private void RegisterCharacterModels()
        {
            var character = _gameData.Characters;
            Container
                .Bind<Dictionary<string, CharacterModel>>()
                .FromInstance(character)
                .AsSingle();
        }

        private void RegisterWeaponModels()
        {
            var weapons = _gameData.Weapons;
            Container
                .Bind<Dictionary<string, WeaponModel>>()
                .FromInstance(weapons)
                .AsSingle();
        }

        private void RegisterSetting()
        {
            Container
                .Bind<GameSetting>()
                .FromInstance(_setting)
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
                .Bind<ProjectStateMachine>()
                .FromNew()
                .AsSingle()
                .NonLazy();
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
                .AsSingle()
                .NonLazy();
        }

        private void RegisterUIPool()
        {
            Container
                .Bind<UIElementPool>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void RegisterUIFactory()
        {
            Container
                .Bind<UIFactory>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}