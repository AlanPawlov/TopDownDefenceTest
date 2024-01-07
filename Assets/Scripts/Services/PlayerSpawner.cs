using System.Threading.Tasks;
using Character;
using Environment;
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
        private CharacterPool _pool;
        private Character.Character _player;
        private string _playerModelId;

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
            var position = _playerSpawnPoint.transform.position;
            var rotation = Quaternion.identity;

            var player = _pool.LoadFromPool<Character.Character>(_playerModelId, position, rotation);
            if (player == null)
                player = await _factory.Create(CharacterType.Player, _playerModelId, position, rotation);

            _player = player;
            Events.EventBus.RaiseEvent<ISpawnCharacterHandler>(h => h.HandleSpawnPlayer(player));
        }

        public void Kill()
        {
            _player.Death();
        }
    }
}