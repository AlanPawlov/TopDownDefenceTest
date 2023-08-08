using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Events.Handlers;
using Factories;
using Pools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Services
{
    public class EnemySpawner : IDisposable,IDeathHandler
    {
        private float _maxSpawnDelay = 3;
        private float _minSpawnDelay = 1.8f;
        private float _spawnDelay;
        private float _spawnTimer;

        private int _maxEnemySpawned = 30;
        private int _curEnemySpawned;
        private bool _isWork;

        private CharacterFactory _factory;
        private CharacterPool _pool;
        private EnemySpawnPoint[] _spawnPoints;
        private UpdateSender _updateSender;
        private string _enemyPath = "Prefabs/Player";
        private List<Character> _characters;

        public EnemySpawner(UpdateSender updateSender, CharacterFactory factory,CharacterPool characterPool, EnemySpawnPoint[] spawnPoints)
        {
            _characters = new List<Character>();
            _updateSender = updateSender;
            _factory = factory;
            _pool = characterPool;
            _spawnPoints = spawnPoints;
            _updateSender.OnUpdate += OnUpdate;
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
                _curEnemySpawned++;
                if (_curEnemySpawned >= +_maxEnemySpawned)
                {
                    _isWork = false;
                    return;
                }

                SetSpawnDelay();
            }
        }

        private async Task SpawnEnemy()
        {
            var pointIndex = Random.Range(0, _spawnPoints.Length);
            var enemy = _pool.LoadFromPool<Character>(_enemyPath, _spawnPoints[pointIndex].transform.position,
                Quaternion.identity);
            if (enemy == null)
            enemy = await _factory.Create(CharacterType.Enemy, _enemyPath,
                _spawnPoints[pointIndex].transform.position);
            Events.EventBus.RaiseEvent<ISpawnCharacterHandler>(h => h.HandleSpawnEnemy(enemy));
        }

        private void SetSpawnDelay()
        {
            _spawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
            _spawnTimer = _spawnDelay;
        }

        
        public void KillAll()
        {
            for (int i = _characters.Count - 1; i > 0; i--)
            {
                HandleDeath(_characters[i]);
            }
        }
        
        public void Dispose()
        {
            _updateSender = null;
            _factory = null;
            _spawnPoints = null;
            _updateSender.OnUpdate -= OnUpdate;
            _isWork = true;
        }


        public void HandleDeath(Character character)
        {
            if (_characters.Contains(character))
                _characters.Remove(character);
        }
    }
}