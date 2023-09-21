using System;
using System.Collections.Generic;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.BaitSystem
{
    public class BaitRegistry : IInitializable, IDisposable
    {
        private readonly List<BaitFacade> _baits = new();
        
        public IEnumerable<BaitFacade> Baits => _baits;
        
        private readonly SignalBus _signalBus;
        
        public BaitRegistry(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void AddBait(BaitFacade bait)
        {
            _baits.Add(bait);
        }

        public void RemoveBait(BaitFacade bait)
        {
            _baits.Remove(bait);
        }

        public void Initialize()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<OnResetGameSignal>(OnResetGame);
        }
        
        private void OnResetGame()
        {
            for(var i = _baits.Count - 1; i >= 0; i--)
            {
                _baits[i].Dispose();
            }
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<OnResetGameSignal>(OnResetGame);
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}