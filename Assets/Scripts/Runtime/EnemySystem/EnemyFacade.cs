using System;
using UnityEngine;
using Zenject;

namespace Runtime.EnemySystem
{
    public class EnemyFacade : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;
        
        private EnemyView _enemyView;
        
        private EnemyRegistry _enemyRegistry;
        
        private EnemyDestroyHandler _enemyDestroyHandler;
        
        private EnemyTunable _enemyTunable;
        
        [Inject]
        public void Construct(
            EnemyView view, 
            EnemyRegistry registry, 
            EnemyDestroyHandler destroyHandler,
            EnemyTunable tunable)
        {
            _enemyView = view;
            _enemyRegistry = registry;
            _enemyDestroyHandler = destroyHandler;
            _enemyTunable = tunable;
        }
        
        public Vector3 Position
        {
            get => _enemyView.Position;
            set => _enemyView.Position = value;
        }
        
        public Vector3 Direction
        {
            get => _enemyView.Direction;
            set => _enemyView.Direction = value;
        }
        
        public int ScoreValue
        {
            get => _enemyTunable.FishScore;
        }
        
        public bool IsEatable
        {
            get => _enemyView.IsEatable;
            set => _enemyView.IsEatable = value;
        }
        public void OnDespawned()
        {
            _enemyRegistry.RemoveBait(this);
            _pool = null;
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
            _enemyRegistry.AddBait(this);
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }
        
        public void Destroy()
        {
            _enemyDestroyHandler.Destroy();
        }
        
        public class Factory : PlaceholderFactory<EnemyFacade>
        {
            
        }
    }
}