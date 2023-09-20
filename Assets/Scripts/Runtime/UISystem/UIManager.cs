using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.UISystem
{
    public class UIManager : MonoBehaviour
    {
        private SignalBus _signalBus;
        
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Start()
        {
            OnOpenStartPanel();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            
        }
        
        private void UnsubscribeEvents()
        {
            
        }
        
        private void OnOpenStartPanel()
        {
            _signalBus.Fire(new OnOpenPanelSignal
            {
                PanelType = UIPanelTypes.LevelPanel, 
                PanelIndex = 0
            });
        }
    }
}