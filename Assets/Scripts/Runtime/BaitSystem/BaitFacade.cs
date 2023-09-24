using System;
using Runtime.Interfaces;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.BaitSystem
{
    public class BaitFacade : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable, IEatable
    {
        private IMemoryPool _pool;
        
        private BaitView _baitView;
        
        private BaitRegistry _baitRegistry;
        
        private BaitDestroyHandler _baitDestroyHandler;
        
        private BaitTunable _baitTunable;
        
        private BaitPhysicHandler _baitPhysicHandler;
        
        private SignalBus _signalBus;
        
        
        [Inject]
        public void Construct(
            BaitView baitView, 
            BaitRegistry baitRegistry, 
            BaitDestroyHandler baitDestroyHandler,
            BaitTunable baitTunable,
            BaitPhysicHandler baitPhysicHandler,
            SignalBus signalBus)
        {
            _baitView = baitView;
            _baitRegistry = baitRegistry;
            _baitDestroyHandler = baitDestroyHandler;
            _baitTunable = baitTunable;
            _baitPhysicHandler = baitPhysicHandler;
            _signalBus = signalBus;
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

        public void Eat()
        {
            _signalBus.Fire(new IncreaseScoreSignal()
            {
                ScoreValue = _baitTunable.BaitScore
            });
                
            _signalBus.Fire(new UpdateStageImageFillAmountSignal()
            {
                FillAmount = _baitTunable.BaitScore
            });

            Destroy();
        }

        public class Factory : PlaceholderFactory<BaitFacade>
        {

        }
    }
}