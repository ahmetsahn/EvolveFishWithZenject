using System;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.UISystem
{
    public enum UIEventSubscriptionType
    {
        NextLevel,
        RestartLevel,
        Quit
    }
    public class UIEventSubscriber : MonoBehaviour
    {
        [SerializeField] 
        private UIEventSubscriptionType subscriptionType;
        
        [SerializeField]
        private Button button;
        
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
        
        private void SubscribeEvents()
        {
            switch (subscriptionType)
            {
                case UIEventSubscriptionType.NextLevel:
                    button.onClick.AddListener(OnNextLevel);
                    break;
                case UIEventSubscriptionType.RestartLevel:
                    button.onClick.AddListener(OnRestartLevel);
                    break;
                case UIEventSubscriptionType.Quit:
                    button.onClick.AddListener(OnQuit);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnNextLevel()
        {
            _signalBus.Fire<NextLevelButtonClickSignal>();
        }
        
        private void OnRestartLevel()
        {
            _signalBus.Fire<RestartLevelButtonClickSignal>();
        }
        
        private void OnQuit()
        {
            _signalBus.Fire<QuitButtonClickSignal>(); 
        }
        
        private void UnsubscribeEvents()
        {
            switch (subscriptionType)
            {
                case UIEventSubscriptionType.NextLevel:
                    button.onClick.RemoveListener(OnNextLevel);
                    break;
                case UIEventSubscriptionType.RestartLevel:
                    button.onClick.RemoveListener(OnRestartLevel);
                    break;
                case UIEventSubscriptionType.Quit:
                    button.onClick.RemoveListener(OnQuit);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}