using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Common.Events;
using Common.Events.Handlers;
using Game.Character;
using UnityEngine;

namespace Game.Environment
{
    public class PlayerSpawner
    {
        private CharacterFactory _factory;
        private PlayerSpawnPoint _playerSpawnPoint;
        private CharacterPool _pool;
        private Character.Character _player;
        private string _playerModelId;

        public PlayerSpawner(CharacterFactory factory, CharacterPool characterPool, PlayerSpawnPoint spawnPoint,
            GameData gameData)
        {
            _playerSpawnPoint = spawnPoint;
            _factory = factory;
            _pool = characterPool;
            _playerModelId = gameData.WallDefenceRules.First().Value.PalyerCharacterId;
        }

        public async Task Spawn()
        {
            var position = _playerSpawnPoint.transform.position;
            var rotation = Quaternion.identity;

            var player = _pool.LoadFromPool<Character.Character>(_playerModelId, position, rotation);
            if (player == null)
                player = await _factory.Create(CharacterType.Player, _playerModelId, position, rotation);

            _player = player;
            EventBus.RaiseEvent<ISpawnCharacterHandler>(h => h.HandleSpawnPlayer(player));
        }

        public void Kill()
        {
            _player.Death();
        }
    }
}