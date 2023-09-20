using System;
using UnityEngine;
using Zenject;

namespace Runtime.EnemySystem
{
    public class EnemyFacade : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;
        
        private EnemyView _view;
        
        private EnemyRegistry _registry;
        
        private EnemyDestroyHandler _destroyHandler;
        
        private EnemyTunable _tunable;
        
        [Inject]
        public void Construct(
            EnemyView view, 
            EnemyRegistry registry, 
            EnemyDestroyHandler destroyHandler,
            EnemyTunable tunable)
        {
            _view = view;
            _registry = registry;
            _destroyHandler = destroyHandler;
            _tunable = tunable;
        }
        
        public Vector3 Position
        {
            get => _view.Position;
            set => _view.Position = value;
        }
        
        public int ScoreValue
        {
            get => _tunable.FishScore;
        }
        public void OnDespawned()
        {
            _registry.RemoveBait(this);
            _pool = null;
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
            _registry.AddBait(this);
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }
        
        public void Destroy()
        {
            _destroyHandler.Destroy();
        }
        
        public class Factory : PlaceholderFactory<EnemyFacade>
        {
            
        }
    }
}