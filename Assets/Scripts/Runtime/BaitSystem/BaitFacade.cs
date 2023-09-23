using System;
using UnityEngine;
using Zenject;

namespace Runtime.BaitSystem
{
    public class BaitFacade : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;
        
        private BaitView _baitView;
        
        private BaitRegistry _baitRegistry;
        
        private BaitDestroyHandler _baitDestroyHandler;
        
        private BaitTunable _baitTunable;
        
        private BaitPhysicHandler _baitPhysicHandler;
        
        
        [Inject]
        public void Construct(
            BaitView baitView, 
            BaitRegistry baitRegistry, 
            BaitDestroyHandler baitDestroyHandler,
            BaitTunable baitTunable,
            BaitPhysicHandler baitPhysicHandler)
        {
            _baitView = baitView;
            _baitRegistry = baitRegistry;
            _baitDestroyHandler = baitDestroyHandler;
            _baitTunable = baitTunable;
            _baitPhysicHandler = baitPhysicHandler;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _baitPhysicHandler.OnTriggerEnter2D(other);
        }

        public Vector3 Position
        {
            get => _baitView.Position;
            set => _baitView.Position = value;
        }

        public int ScoreValue
        {
            get => _baitTunable.BaitScore;
        }
        
        public void OnDespawned()
        {
            _baitRegistry.RemoveBait(this);
            _pool = null;
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
            
            _baitRegistry.AddBait(this);
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }

        public void Destroy()
        {
            _baitDestroyHandler.Destroy();
        }
        
        public class Factory : PlaceholderFactory<BaitFacade>
        {
            
        }
    }
}