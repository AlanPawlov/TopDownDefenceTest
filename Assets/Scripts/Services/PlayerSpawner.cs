using System.Threading.Tasks;
using Events.Handlers;
using Factories;
using Pools;
using SO;
using UnityEngine;

namespace Services
{
    public class PlayerSpawner
    {
        private CharacterFactory _factory;
        private PlayerSpawnPoint _playerSpawnPoint;
        private string _playerModelId;
        private CharacterPool _pool;
        private Character _player;

        public PlayerSpawner(CharacterFactory factory, CharacterPool characterPool, PlayerSpawnPoint spawnPoint,
            GameSetting setting)
        {
            _playerSpawnPoint = spawnPoint;
            _factory = factory;
            _pool = characterPool;
            _playerModelId = setting.PalyerCharacterId;
        }

        public async Task Spawn()
        {
            var player = (Character)_pool.LoadFromPool<Character>(_playerModelId);
            if (player == null)
                player = await _factory.Create(CharacterType.Player, _playerModelId,
                    _playerSpawnPoint.transform.position, Quaternion.identity);

            _player = player;
            Events.EventBus.RaiseEvent<ISpawnCharacterHandler>(h => h.HandleSpawnPlayer(player));
        }

        public void Kill()
        {
            _player.Death();
        }
    }
}