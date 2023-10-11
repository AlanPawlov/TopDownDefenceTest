using Factories;
using Pools;
using Services;
using States.MatchStates;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplayServiceInstaller : MonoInstaller
    {
        [SerializeField] private WallDefenceBootstrap _wallDefenceBootstrap;

        public override void InstallBindings()
        {
            RegisterPlayerMovement();
            RegisterCharacterFactory();
            RegisterEnemySpawner();
            RegisterEnemyMovement();
            RegisterPlayerAttack();
            RegisterEnemyAttack();
            RegisterProjectileFactory();
            RegisterPlayerSpawner();
            RegisterCharacterPool();
            RegisterProjectilePool();
            RegisterStateMachine();
            // RegisterInitializibleInterface();
        }

        private void RegisterPlayerMovement()
        {
            Container
                .Bind<PlayerMovementService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void RegisterPlayerSpawner()
        {
            Container
                .Bind<PlayerSpawner>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void RegisterEnemyMovement()
        {
            Container
                .Bind<EnemyMovementService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void RegisterPlayerAttack()
        {
            Container
                .Bind<PlayerAttackService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void RegisterEnemyAttack()
        {
            Container
                .Bind<EnemyAttackService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void RegisterProjectileFactory()
        {
            Container
                .Bind<ProjectileFactory>()
                .FromNew()
                .AsSingle();
        }

        private void RegisterEnemySpawner()
        {
            Container
                .Bind<EnemySpawner>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void RegisterCharacterFactory()
        {
            Container
                .Bind<CharacterFactory>()
                .FromNew()
                .AsSingle();
        }

        private void RegisterProjectilePool()
        {
            Container
                .Bind<ProjectilePool>()
                .FromNew()
                .AsSingle();
        }

        private void RegisterCharacterPool()
        {
            Container
                .Bind<CharacterPool>()
                .FromNew()
                .AsSingle();
        }

        private void RegisterStateMachine()
        {
            Container
                .Bind<MatchStateMachine>()
                .AsSingle()
                .NonLazy();
        }

        // private void RegisterInitializibleInterface()
        // {
        //     Container
        //         .BindInterfacesTo<WallDefenceBootstrap>()
        //         .FromInstance(_wallDefenceBootstrap)
        //         .AsSingle();
        // }
    }
}