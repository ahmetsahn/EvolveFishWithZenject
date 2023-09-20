using System;
using UnityEngine;
using Zenject;

namespace Runtime.BaitSystem
{
    public class BaitFacade : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;
        
        private BaitView _view;
        
        private BaitRegistry _registry;
        
        private BaitDestroyHandler _destroyHandler;
        
        private BaitTunable _tunable;
        
        private BaitPhysicHandler _physicHandler;
        
        
        [Inject]
        public void Construct(
            BaitView view, 
            BaitRegistry registry, 
            BaitDestroyHandler destroyHandler,
            BaitTunable tunable,
            BaitPhysicHandler physicHandler)
        {
            _view = view;
            _registry = registry;
            _destroyHandler = destroyHandler;
            _tunable = tunable;
            _physicHandler = physicHandler;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _physicHandler.OnTriggerEnter2D(other);
        }

        public Vector3 Position
        {
            get => _view.Position;
            set => _view.Position = value;
        }

        public int ScoreValue
        {
            get => _tunable.BaitScore;
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
        
        public class Factory : PlaceholderFactory<BaitFacade>
        {
            
        }
    }
}