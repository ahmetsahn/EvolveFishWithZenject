using System;
using Runtime.Enums;
using Runtime.Signals;
using Zenject;

namespace Runtime.Main
{
    public enum GameStates
    {
        Playing,
        Win,
        Lose
    }
    public class GameManager : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        
        public GameStates GameStates { get; private set; }

        public GameManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            SubscribeEvents();
            
            GameStates = GameStates.Playing;
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<OnChangeGameStatesSignal>(OnChangeGameStates);
        }
        
        private void OnChangeGameStates(OnChangeGameStatesSignal signal)
        {
            GameStates = signal.GameStates;

            switch (GameStates)
            {
                case GameStates.Playing:
                    break;
                
                case GameStates.Win:
                    
                    _signalBus.Fire<OnLevelDestroySignal>();
                    _signalBus.Fire<OnCloseAllPanelsSignal>();
                    _signalBus.Fire(new OnOpenPanelSignal
                    {
                        PanelType = UIPanelTypes.WinPanel, 
                        PanelIndex = 1
                    });
                    break;
                
                case GameStates.Lose:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<OnChangeGameStatesSignal>(OnChangeGameStates);
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}