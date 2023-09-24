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
            _signalBus.Fire(new LevelStartSignal()
            {
                LevelIndex = _currentLevelIndex
            });
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<LevelStartSignal>(OnLevelStart);
            _signalBus.Subscribe<LevelDestroySignal>(OnLevelDestroy);
            _signalBus.Subscribe<NextLevelSignal>(OnNextLevel);
            _signalBus.Subscribe<RestartLevelSignal>(OnRestartLevel);
            _signalBus.Subscribe<ResetGameSignal>(OnResetGame);
        }
        
        private void OnLevelStart(LevelStartSignal signal)
        {
            var levelPrefab = _levelLoaderCommand.Execute(signal.LevelIndex);
            _container.InjectGameObject(levelPrefab);
        }
        
        private void OnLevelDestroy()
        {
            _levelDestroyerCommand.Execute();
        }
        
        private void OnNextLevel()
        {
            _currentLevelIndex++;
            
            _signalBus.Fire<ResetGameSignal>();
            
            _signalBus.Fire(new ChangeGameStatesSignal()
            {
                GameStates = GameStates.Playing
            });
            
            _signalBus.Fire(new OpenPanelSignal()
            {
                PanelType = UIPanelTypes.LevelPanel,
                PanelIndex = 0
            });
            
            _signalBus.Fire(new LevelStartSignal()
            {
                LevelIndex = _currentLevelIndex
            });
        }
        
        private void OnRestartLevel()
        {
            _signalBus.Fire<ResetGameSignal>();
            
            _signalBus.Fire(new ChangeGameStatesSignal()
            {
                GameStates = GameStates.Playing
            });
            
            _signalBus.Fire(new OpenPanelSignal()
            {
                PanelType = UIPanelTypes.LevelPanel,
                PanelIndex = 0
            });
            
            _signalBus.Fire(new LevelStartSignal()
            {
                LevelIndex = _currentLevelIndex
            });
        }
        
        private void OnResetGame()
        {
            _signalBus.Fire(new ClosePanelSignal()
            {
                PanelIndex = 1
            });
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<LevelStartSignal>(OnLevelStart);
            _signalBus.Unsubscribe<LevelDestroySignal>(OnLevelDestroy);
            _signalBus.Unsubscribe<NextLevelSignal>(OnNextLevel);
            _signalBus.Unsubscribe<RestartLevelSignal>(OnRestartLevel);
            _signalBus.Unsubscribe<ResetGameSignal>(OnResetGame);
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}