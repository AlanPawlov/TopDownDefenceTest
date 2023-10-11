using System.Collections.Generic;
using Models;
using Resource;
using Services;
using SO;
using States.GameStates;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CommonServiceInstaller : MonoInstaller
    {
        [SerializeField] private UpdateSender _updateSender;
        [SerializeField] private MainCanvas _globalCanvas;
        [SerializeField] private AllWeapons _weapons;
        [SerializeField] private AllCharacters _characters;
        [SerializeField] private AllEnemySpawners _spawners;
        [SerializeField] private AllWalls _walls;
        [SerializeField] private AllProjectiles _projectiles;
        [SerializeField] private GameSetting _setting;
        [SerializeField] private ProjectBootstrap _projectBootstrap;

        public override void InstallBindings()
        {
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

        private void RegisterInitializable()
        {
            Container
                .BindInterfacesTo<ProjectBootstrap>()
                .FromInstance(_projectBootstrap)
                .AsSingle();
        }

        private void RegisterProjectileModels()
        {
            var projectiles = new Dictionary<string, ProjectileModel>();
            foreach (var item in _projectiles.Projectiles)
            {
                projectiles.Add(item.Id, item);
            }

            Container
                .Bind<Dictionary<string, ProjectileModel>>()
                .FromInstance(projectiles)
                .AsSingle();
        }

        private void RegisterWallModels()
        {
            var walls = new Dictionary<string, WallModel>();
            foreach (var item in _walls.Walls)
            {
                walls.Add(item.Id, item);
            }

            Container
                .Bind<Dictionary<string, WallModel>>()
                .FromInstance((walls))
                .AsSingle();
        }

        private void RegisterEnemySpawners()
        {
            var spawners = new Dictionary<string, EnemySpawnerModel>();
            foreach (var item in _spawners.EnemySpawners)
            {
                spawners.Add(item.Id, item);
            }

            Container
                .Bind<Dictionary<string, EnemySpawnerModel>>()
                .FromInstance(spawners)
                .AsSingle();
        }

        private void RegisterCharacterModels()
        {
            var character = new Dictionary<string, CharacterModel>();
            foreach (var item in _characters.Characters)
            {
                character.Add(item.Id, item);
            }

            Container
                .Bind<Dictionary<string, CharacterModel>>()
                .FromInstance(character)
                .AsSingle();
        }

        private void RegisterWeaponModels()
        {
            var weapons = new Dictionary<string, WeaponModel>();
            foreach (var item in _weapons.Weapons)
            {
                weapons.Add(item.Id, item);
            }

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
                .To<ResourceLoader>()
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