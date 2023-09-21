using System;
using Runtime.Main;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Runtime.BaitSystem
{
    public class BaitSpawner : IInitializable, ITickable, IDisposable
    {
        private readonly BaitFacade.Factory _baitFactory;
        
        private readonly SpawnerData _spawnerData;
        
        private readonly GameManager _gameManager;
        
        private readonly SignalBus _signalBus;
        
        private float _lastSpawnTime;
        
        public BaitSpawner(
            BaitFacade.Factory baitFactory, 
            SpawnerData spawnerData,
            GameManager gameManager,
            SignalBus signalBus)
        {
            _baitFactory = baitFactory;
            _spawnerData = spawnerData;
            _gameManager = gameManager;
            _signalBus = signalBus;
        }
        public void Initialize()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            
        }
        
        private void UnsubscribeEvents()
        {
            
        }

        public void Tick()
        {
            if (_gameManager.GameStates != GameStates.Playing) return;
            
            if(Time.realtimeSinceStartup - _lastSpawnTime > _spawnerData.SpawnInterval)
            {
                SpawnBait();
            }
        }
        
        private void SpawnBait()
        {
            var baitFacade = _baitFactory.Create();
            baitFacade.Position = ChooseRandomStartPosition();
            _lastSpawnTime = Time.realtimeSinceStartup;
        }
        
        private Vector2 ChooseRandomStartPosition()
        {
            var randomX = Random.Range(_spawnerData.MinX, _spawnerData.MaxX);
            var y = _spawnerData.Y;
            return new Vector3(randomX, y);
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
        
        [Serializable]
        public struct SpawnerData
        {
            public float SpawnInterval;
            public float MinX;
            public float MaxX;
            public float Y;
        }
    }
}