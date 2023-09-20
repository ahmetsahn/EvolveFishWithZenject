using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Runtime.EnemySystem
{
    public class EnemySpawner : IInitializable, ITickable, IDisposable
    {
        private readonly EnemyFacade.Factory _enemyFactory;
        
        private readonly Settings _spawnerData;
        
        private float _lastSpawnTime;
        
        public EnemySpawner(EnemyFacade.Factory enemyFactory, Settings spawnerData)
        {
            _enemyFactory = enemyFactory;
            _spawnerData = spawnerData;
        }
        public void Initialize()
        {
            SubscribeEvents();
        }

        public void Tick()
        {
            if(Time.realtimeSinceStartup - _lastSpawnTime > _spawnerData.SpawnInterval)
            {
                SpawnBait();
            }
        }
        
        private void SpawnBait()
        {
            var enemyFacade = _enemyFactory.Create();
            enemyFacade.Position = ChooseRandomStartPosition();
            _lastSpawnTime = Time.realtimeSinceStartup;
        }
        
        private Vector2 ChooseRandomStartPosition()
        {
            var side = Random.Range(0, 2);
            var randomX = side == 0 ? _spawnerData.X : -_spawnerData.X;
            var randomY = Random.Range(_spawnerData.MinY, _spawnerData.MaxY);
            return new Vector3(randomX, randomY);
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            
        }
        
        private void UnsubscribeEvents()
        {
            
        }
        
        [Serializable]
        public struct Settings
        {
            public float SpawnInterval;
            public float X;
            public float MinY;
            public float MaxY;
        }
    }
}