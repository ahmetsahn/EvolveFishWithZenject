using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Runtime.EnemySystem
{
    public class EnemySpawner : ITickable
    {
        private readonly EnemyFacade.Factory _enemyFactory;
        
        private readonly EnemySpawnerData _enemySpawnerData;
        
        private float _lastSpawnTime;
        
        public EnemySpawner(EnemyFacade.Factory enemyFactory, EnemySpawnerData enemySpawnerData)
        {
            _enemyFactory = enemyFactory;
            _enemySpawnerData = enemySpawnerData;
        }
        
        public void Tick()
        {
            if(Time.realtimeSinceStartup - _lastSpawnTime > _enemySpawnerData.SpawnInterval)
            {
                SpawnEnemy();
            }
        }
        
        private void SpawnEnemy()
        {
            var enemyFacade = _enemyFactory.Create();
            var startPosition = ChooseRandomStartPosition();
            enemyFacade.Position = startPosition;
            enemyFacade.Direction = startPosition.x > 0 ? Vector3.left : Vector3.right;
            _lastSpawnTime = Time.realtimeSinceStartup;
        }
        
        private Vector2 ChooseRandomStartPosition()
        {
            var side = Random.Range(0, 2);
            var randomX = side == 0 ? _enemySpawnerData.X : -_enemySpawnerData.X;
            var randomY = Random.Range(_enemySpawnerData.MinY, _enemySpawnerData.MaxY);
            return new Vector3(randomX, randomY);
        }
        
        [Serializable]
        public struct EnemySpawnerData
        {
            public float SpawnInterval;
            public float X;
            public float MinY;
            public float MaxY;
        }
    }
}