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
            OnOpenLevelPanel();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<NextLevelButtonClickSignal>(OnNextLevelButton);
            _signalBus.Subscribe<RestartLevelButtonClickSignal>(OnRestartLevelButton);
            _signalBus.Subscribe<QuitButtonClickSignal>(OnQuitButton);
        }
        
        private void OnNextLevelButton()
        {
            _signalBus.Fire<NextLevelSignal>();
        }
        
        private void OnRestartLevelButton()
        {
            _signalBus.Fire<RestartLevelSignal>();
        }
        
        private void OnQuitButton()
        {
            Application.Quit();
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<NextLevelButtonClickSignal>(OnNextLevelButton);
            _signalBus.Unsubscribe<RestartLevelButtonClickSignal>(OnRestartLevelButton);
            _signalBus.Unsubscribe<QuitButtonClickSignal>(OnQuitButton);
        }
        
        private void OnOpenLevelPanel()
        {
            _signalBus.Fire(new OpenPanelSignal
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