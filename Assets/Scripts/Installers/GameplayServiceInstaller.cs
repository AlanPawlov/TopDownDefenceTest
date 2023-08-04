using Factories;
using Services;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplayServiceInstaller : MonoInstaller
    {
        [SerializeField] private EnemySpawnPoint[] _enemySpawnPoint;
        [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;
        [SerializeField] private Wall _wall;
        
        public override void InstallBindings()
        {
            BindWall();
            BindPlayerSpawnPoint();
            BindEnemySpawnPoints();
            BindPlayerMovement();
            BindCharacterFactory();
            BindEnemySpawner();
            BindEnemyMovement();
            BindPlayerAttack();
            BindEnemyAttack();
            BindProjectileFactory();
            BindPlayerSpawner();
        }

        private void BindPlayerSpawnPoint()
        {
            Container
                .Bind<PlayerSpawnPoint>()
                .FromInstance(_playerSpawnPoint)
                .AsSingle();
        }

        private void BindWall()
        {
            Container
                .Bind<Wall>()
                .FromInstance(_wall)
                .AsSingle();
        }

        private void BindEnemySpawnPoints()
        {
            Container
                .Bind<EnemySpawnPoint[]>()
                .FromInstance(_enemySpawnPoint)
                .AsSingle();
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
    }
}