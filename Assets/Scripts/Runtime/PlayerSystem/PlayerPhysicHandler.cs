using Runtime.BaitSystem;
using Runtime.Signals;
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
            if (other.TryGetComponent(out BaitFacade bait))
            {
                _signalBus.Fire(new OnIncreaseScoreSignal()
                {
                    ScoreValue = bait.ScoreValue
                });
                
                _signalBus.Fire(new OnUpdateStageImageFillAmountSignal()
                {
                    StageIndex = new OnUpdateStageIndexSignal().StageIndex,
                    FillAmount = bait.ScoreValue
                });
                
                bait.Destroy();
            }
        }
    }
}