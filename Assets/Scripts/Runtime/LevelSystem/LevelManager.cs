using Runtime.Commands.Level;
using Runtime.Enums;
using Runtime.Main;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.LevelSystem
{
    public class LevelManager : MonoBehaviour
    {
        private DiContainer _container;
        
        private SignalBus _signalBus;
        
        [SerializeField] private Transform levelRoot;   
        
        private LevelLoaderCommand _levelLoaderCommand;
        
        private LevelDestroyerCommand _levelDestroyerCommand;
        
        private int _currentLevelIndex;
        
        
        [Inject]
        public void Construct(
            DiContainer container, 
            SignalBus signalBus)
        {
            _container = container;
            _signalBus = signalBus;
        }
        
        private void Awake()
        {
            Init();
        }
        
        private void Init()
        {
            _levelLoaderCommand = new LevelLoaderCommand(ref levelRoot);
            _levelDestroyerCommand = new LevelDestroyerCommand(ref levelRoot);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Start()
        {
            _signalBus.Fire(new OnLevelStartSignal()
            {
                LevelIndex = _currentLevelIndex
            });
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<OnLevelStartSignal>(OnLevelStart);
            _signalBus.Subscribe<OnLevelDestroySignal>(OnLevelDestroy);
            _signalBus.Subscribe<OnNextLevelButtonClickSignal>(OnNextLevelButtonClick);
            _signalBus.Subscribe<OnResetGameSignal>(OnResetGame);
        }
        
        private void OnLevelStart(OnLevelStartSignal signal)
        {
            var levelPrefab = _levelLoaderCommand.Execute(signal.LevelIndex);
            _container.InjectGameObject(levelPrefab);
        }
        
        private void OnLevelDestroy()
        {
            _levelDestroyerCommand.Execute();
        }
        
        private void OnNextLevelButtonClick()
        {
            _signalBus.Fire<OnResetGameSignal>();
            
            _currentLevelIndex++;
            
            _signalBus.Fire(new OnChangeGameStatesSignal()
            {
                GameStates = GameStates.Playing
            });
            
            _signalBus.Fire(new OnOpenPanelSignal()
            {
                PanelType = UIPanelTypes.LevelPanel,
                PanelIndex = 0
            });
            
            _signalBus.Fire(new OnLevelStartSignal()
            {
                LevelIndex = _currentLevelIndex
            });
        }
        
        private void OnResetGame()
        {
            _signalBus.Fire(new OnClosePanelSignal()
            {
                PanelIndex = 1
            });
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<OnLevelStartSignal>(OnLevelStart);
            _signalBus.Unsubscribe<OnLevelDestroySignal>(OnLevelDestroy);
            _signalBus.Unsubscribe<OnNextLevelButtonClickSignal>(OnNextLevelButtonClick);
            _signalBus.Unsubscribe<OnResetGameSignal>(OnResetGame);
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}