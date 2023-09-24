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
            _signalBus.Subscribe<ChangeGameStatesSignal>(OnChangeGameStates);
        }
        
        private void OnChangeGameStates(ChangeGameStatesSignal signal)
        {
            GameStates = signal.GameStates;

            switch (GameStates)
            {
                case GameStates.Playing:
                    break;
                
                case GameStates.Win:
                    
                    _signalBus.Fire<LevelDestroySignal>();
                    _signalBus.Fire<CloseAllPanelsSignal>();
                    _signalBus.Fire(new OpenPanelSignal
                    {
                        PanelType = UIPanelTypes.WinPanel, 
                        PanelIndex = 1
                    });
                    break;
                
                case GameStates.Lose:
                    _signalBus.Fire<LevelDestroySignal>();
                    _signalBus.Fire<CloseAllPanelsSignal>();
                    _signalBus.Fire(new OpenPanelSignal
                    {
                        PanelType = UIPanelTypes.LosePanel, 
                        PanelIndex = 1
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<ChangeGameStatesSignal>(OnChangeGameStates);
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}