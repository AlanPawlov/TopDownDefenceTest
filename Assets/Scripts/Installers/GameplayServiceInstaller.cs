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

        public override void InstallBindings()
        {
            BindPlayerSpawnPoint();
            BindEnemySpawnPoints();
            BindPlayerMovement();
            BindPlayerSpawner();
            BindCharacterFactory();
        }

        private void BindPlayerSpawnPoint()
        {
            Container
                .Bind<PlayerSpawnPoint>()
                .FromInstance(_playerSpawnPoint)
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

        private void BindCharacterFactory()
        {
            Container
                .Bind<CharacterFactory>()
                .FromNew()
                .AsSingle();
        }
    }
}