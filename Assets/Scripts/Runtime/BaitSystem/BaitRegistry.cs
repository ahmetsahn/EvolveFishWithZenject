using System;
using System.Collections.Generic;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.BaitSystem
{
    public class BaitRegistry : IDisposable
    {
        private readonly List<BaitFacade> _baitFacadesList = new();
        
        public IEnumerable<BaitFacade> BaitFacadesList => _baitFacadesList;
        
        private readonly SignalBus _signalBus;
        
        public BaitRegistry(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            SubscribeEvents();
        }
        
        public void AddBait(BaitFacade bait)
        {
            _baitFacadesList.Add(bait);
        }

        public void RemoveBait(BaitFacade bait)
        {
            _baitFacadesList.Remove(bait);
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<OnResetGameSignal>(OnResetGame);
        }
        
        private void OnResetGame()
        {
            for(var i = _baitFacadesList.Count - 1; i >= 0; i--)
            {
                _baitFacadesList[i].Dispose();
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