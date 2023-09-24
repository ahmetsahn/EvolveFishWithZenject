using Runtime.Interfaces;
using UnityEngine;
using Zenject;

namespace Runtime.PlayerSystem
{
    public class PlayerPhysicHandler
    {
        private readonly SignalBus _signalBus;

        public PlayerPhysicHandler(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IEatable eatable))
            {
                eatable.Eat();
            }
        }
    }
}