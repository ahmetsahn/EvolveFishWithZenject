using System;
using Runtime.Enums;
using Runtime.Main;
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
            OnOpenLevelPanel();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<OnNextLevelButtonClickSignal>(OnNextLevelButton);
            _signalBus.Subscribe<OnRestartLevelButtonClickSignal>(OnRestartLevelButton);
            _signalBus.Subscribe<OnQuitButtonClickSignal>(OnQuitButton);
        }
        
        private void OnNextLevelButton()
        {
            _signalBus.Fire(new OnNextLevelSignal());
        }
        
        private void OnRestartLevelButton()
        {
            _signalBus.Fire(new OnRestartLevelSignal());
        }
        
        private void OnQuitButton()
        {
            Application.Quit();
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<OnNextLevelButtonClickSignal>(OnNextLevelButton);
            _signalBus.Unsubscribe<OnRestartLevelButtonClickSignal>(OnRestartLevelButton);
            _signalBus.Unsubscribe<OnQuitButtonClickSignal>(OnQuitButton);
        }
        
        private void OnOpenLevelPanel()
        {
            _signalBus.Fire(new OnOpenPanelSignal
            {
                PanelType = UIPanelTypes.LevelPanel, 
                PanelIndex = 0
            });
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}