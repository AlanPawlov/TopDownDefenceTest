using Common.Services;
using Common.States;
using Game.Character;
using Game.Environment;
using Game.SceneStates;
using Game.Weapon;
using Zenject;

namespace Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
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
            RegisterBorderHelper();
            RegisterStateMachine();
            RegisterStates();
            ResgisterStatesFactory();
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

        private void RegisterBorderHelper()
        {
            Container
                .Bind<LevelBorderHelper>()
                .FromNew()
                .AsSingle();
        }

        private void RegisterStateMachine()
        {
            Container
                .BindInterfacesAndSelfTo<SceneStateMachine>()
                .AsSingle();
        }

        private void RegisterStates()
        {
            Container
                .Bind<InitSceneState>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<SceneLoopState>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<EndSceneState>()
                .AsSingle()
                .NonLazy();
        }

        private void ResgisterStatesFactory()
        {
            Container
                .Bind<StateFactory>()
                .AsSingle();
        }
    }
}