using System;
using System.Collections.Generic;
using Runtime.Signals;
using Zenject;

namespace Runtime.EnemySystem
{
    public class EnemyRegistry : IDisposable
    {
        private readonly List<EnemyFacade> _enemyFacadesList = new();

        public IEnumerable<EnemyFacade> EnemyFacadesList => _enemyFacadesList;
        
        private readonly SignalBus _signalBus;
        
        public EnemyRegistry(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            SubscribeEvents();
        }

        public void AddBait(EnemyFacade enemy)
        {
            _enemyFacadesList.Add(enemy);
        }

        public void RemoveBait(EnemyFacade enemy)
        {
            _enemyFacadesList.Remove(enemy);
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<ResetGameSignal>(OnResetGame);
        }
        
        private void OnResetGame()
        {
            for(var i = _enemyFacadesList.Count - 1; i >= 0; i--)
            {
                _enemyFacadesList[i].Dispose();
            }
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<ResetGameSignal>(OnResetGame);
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}