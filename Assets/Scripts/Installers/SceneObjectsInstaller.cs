using Environment;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SceneObjectsInstaller : MonoInstaller
    {
        [SerializeField] private EnemySpawnPoint[] _enemySpawnPoint;
        [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;
        [SerializeField] private Wall _wall;

        public override void InstallBindings()
        {
            BindWall();
            BindPlayerSpawnPoint();
            BindEnemySpawnPoints();
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
    }
}