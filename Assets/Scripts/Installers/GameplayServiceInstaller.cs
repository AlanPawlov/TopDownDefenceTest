using Factories;
using Pools;
using Services;
using Services.MatchStates;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplayServiceInstaller : MonoInstaller
    {
        [SerializeField] private GameplaySceneStartup _gameplaySceneStartup;

        public override void InstallBindings()
        {
            BindPlayerMovement();
            BindCharacterFactory();
            BindEnemySpawner();
            BindEnemyMovement();
            BindPlayerAttack();
            BindEnemyAttack();
            BindProjectileFactory();
            BindPlayerSpawner();
            BindCharacterPool();
            BindProjectilePool();
            BindMatcController();
            BindIntefaces();
        }

        private void BindPlayerMovement()
        {
            Container
                .Bind<PlayerMovementService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerSpawner()
        {
            Container
                .Bind<PlayerSpawner>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindEnemyMovement()
        {
            Container
                .Bind<EnemyMovementService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerAttack()
        {
            Container
                .Bind<PlayerAttackService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindEnemyAttack()
        {
            Container
                .Bind<EnemyAttackService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindProjectileFactory()
        {
            Container
                .Bind<ProjectileFactory>()
                .FromNew()
                .AsSingle();
        }

        private void BindEnemySpawner()
        {
            Container
                .Bind<EnemySpawner>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindCharacterFactory()
        {
            Container
                .Bind<CharacterFactory>()
                .FromNew()
                .AsSingle();
        }

        private void BindProjectilePool()
        {
            Container
                .Bind<ProjectilePool>()
                .FromNew()
                .AsSingle();
        }

        private void BindCharacterPool()
        {
            Container
                .Bind<CharacterPool>()
                .FromNew()
                .AsSingle();
        }

        private void BindMatcController()
        {
            Container
                .Bind<MatchController>()
                .FromNew()
                .AsSingle();
        }

        private void BindIntefaces()
        {
            Container
                .BindInterfacesTo<GameplaySceneStartup>()
                .FromInstance(_gameplaySceneStartup)
                .AsSingle();
        }
    }
}