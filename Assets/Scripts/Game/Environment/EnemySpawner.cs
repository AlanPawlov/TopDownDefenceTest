using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Character;
using Common.Data;
using Common.Events;
using Common.Events.Handlers;
using Environment;
using Factories;
using Models;
using Pools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Services
{
    public class EnemySpawner : IDisposable, IDeathHandler
    {
        private float _maxSpawnDelay;
        private float _minSpawnDelay;
        private float _spawnDelay;
        private float _spawnTimer;
        private bool _isWork;
        private List<Character.Character> _characters;
        private EnemySpawnPoint[] _spawnPoints;
        private CharacterFactory _factory;
        private CharacterPool _pool;
        private UpdateSender _updateSender;
        private string _enemyId;
        private string _spawnerId;

        public EnemySpawner(UpdateSender updateSender, CharacterFactory factory, CharacterPool characterPool,
            EnemySpawnPoint[] spawnPoints, GameData gameData)
        {
            _characters = new List<Character.Character>();
            _updateSender = updateSender;
            _factory = factory;
            _pool = characterPool;
            _spawnPoints = spawnPoints;
            var rules = gameData.WallDefenceRules.First().Value;
            _enemyId = rules.EnemyCharacterId;
            _spawnerId = rules.EnemySpawnerId;
            var targetModel = gameData.EnemySpawners[_spawnerId];
            _minSpawnDelay = targetModel.MinSpawnTimeout;
            _maxSpawnDelay = targetModel.MaxSpawnTimeout;
            _updateSender.OnUpdate += OnUpdate;
            EventBus.Subscribe(this);
        }

        public void StartWork()
        {
            _isWork = true;
            SetSpawnDelay();
        }

        public void StopWork()
        {
            _isWork = false;
        }

        private void OnUpdate()
        {
            if (!_isWork)
                return;

            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer <= 0)
            {
                SpawnEnemy();
                SetSpawnDelay();
            }
        }

        private async Task SpawnEnemy()
        {
            var pointIndex = Random.Range(0, _spawnPoints.Length);
            var enemy = _pool.LoadFromPool<Character.Character>(_enemyId, _spawnPoints[pointIndex].transform.position,
                Quaternion.identity);
            if (enemy == null)
                enemy = await _factory.Create(CharacterType.Enemy, _enemyId,
                    _spawnPoints[pointIndex].transform.position);

            _characters.Add(enemy);
            EventBus.RaiseEvent<ISpawnCharacterHandler>(h => h.HandleSpawnEnemy(enemy));
        }

        private void SetSpawnDelay()
        {
            _spawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
            _spawnTimer = _spawnDelay;
        }

        public void KillAll()
        {
            for (int i = _characters.Count - 1; i >= 0; i--)
            {
                _characters[i].Death();
            }
        }

        public void Dispose()
        {
            EventBus.Unsubscribe(this);
            _updateSender.OnUpdate -= OnUpdate;
            _updateSender = null;
            _factory = null;
            _spawnPoints = null;
            _isWork = false;
        }

        public void HandleDeath(Character.Character character)
        {
            if (_characters.Contains(character))
                _characters.Remove(character);
        }
    }
}