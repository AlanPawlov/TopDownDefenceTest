using System.Threading.Tasks;
using Events.Handlers;
using Factories;
using Pools;
using UI;
using UnityEngine;

namespace Services
{
    public class PlayerSpawner
    {
        private CharacterFactory _factory;
        private PlayerSpawnPoint _playerSpawnPoint;
        private string _playerPath = "Prefabs/Player";
        private CharacterPool _pool;

        public PlayerSpawner(CharacterFactory factory, CharacterPool characterPool, PlayerSpawnPoint spawnPoint)
        {
            _playerSpawnPoint = spawnPoint;
            _factory = factory;
            _pool = characterPool;
            Spawn();
        }

        public async Task Spawn()
        {
            var player = (Character)_pool.LoadFromPool<Character>(_playerPath);
            if (player == null)
                player = await _factory.Create(CharacterType.Player, _playerPath, _playerSpawnPoint.transform.position,
                    Quaternion.identity);
            Events.EventBus.RaiseEvent<ISpawnCharacterHandler>(h => h.HandleSpawnPlayer(player));
        }
    }
}