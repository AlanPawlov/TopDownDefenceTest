using System.Threading.Tasks;
using Events.Handlers;
using Factories;
using UnityEngine;

namespace Services
{
    public class PlayerSpawner
    {
        private CharacterFactory _factory;
        private PlayerSpawnPoint _playerSpawnPoint;
        private string _playerPath = "Prefabs/Player";

        public PlayerSpawner(CharacterFactory factory, PlayerSpawnPoint spawnPoint)
        {
            _playerSpawnPoint = spawnPoint;
            _factory = factory;
            Spawn();
        }

        public async Task Spawn()
        {
            var player = await _factory.Create(CharacterType.Player,_playerPath, _playerSpawnPoint.transform.position,
                Quaternion.identity);
            Events.EventBus.RaiseEvent<ISpawnCharacterHandler>(h => h.HandleSpawnPlayer(player));
        }
    }
}