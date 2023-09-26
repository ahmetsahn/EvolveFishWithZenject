using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Enums;
using Runtime.Interfaces;
using Runtime.Main;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.EnemySystem
{
    public class EnemyFacade : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable, IEatable
    {
        private IMemoryPool _pool;
        
        private EnemyView _enemyView;
        
        private EnemyRegistry _enemyRegistry;
        
        private EnemyDestroyHandler _enemyDestroyHandler;
        
        private EnemyTunable _enemyTunable;
        
        private SignalBus _signalBus;

        private FishType _playerFishType;


        [Inject]
        public void Construct(
            EnemyView view, 
            EnemyRegistry registry, 
            EnemyDestroyHandler destroyHandler,
            EnemyTunable tunable,
            SignalBus signalBus)
        {
            _enemyView = view;
            _enemyRegistry = registry;
            _enemyDestroyHandler = destroyHandler;
            _enemyTunable = tunable;
            _signalBus = signalBus;
        }

        private void Awake()
        {
            SubscribeEvents();
        }

        private void Start()
        {
            InitializeEnemyFishScores();
        }

        private void SubscribeEvents()
        {
            _signalBus.Subscribe<GetPlayerFishTypeSignal>(OnGetPlayerFishType);
        }
        
        private void OnGetPlayerFishType(GetPlayerFishTypeSignal signal)
        {
            _playerFishType = signal.FishType;
        }

        private void InitializeEnemyFishScores()
        {
            _enemyView.FishScoreDictionary[FishType.Blue] = _enemyTunable.BlueFishScore;
            _enemyView.FishScoreDictionary[FishType.Red] = _enemyTunable.RedFishScore;
            _enemyView.FishScoreDictionary[FishType.Brown] = _enemyTunable.BrownFishScore;
        }

        public Vector3 Position
        {
            get => _enemyView.Position;
            set => _enemyView.Position = value;
        }
        
        public Vector3 Direction
        {
            get => _enemyView.Direction;
            set => _enemyView.Direction = value;
        }
        
        public void OnDespawned()
        {
            _enemyRegistry.RemoveBait(this);
            _pool = null;
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
            _enemyRegistry.AddBait(this);
        }

        public void Eat()
        {
            if (_enemyView.EatableFishTypes
                .Contains(_playerFishType))
            {
                _signalBus.Fire(new ChangeGameStatesSignal()
                {
                    GameStates = GameStates.Lose
                });
            }

            else
            {
                _signalBus.Fire(new IncreaseScoreSignal()
                {
                    ScoreValue = _enemyView.FishScoreDictionary[_enemyView.EnemyFishType]
                });

                _signalBus.Fire(new UpdateStageImageFillAmountSignal()
                {
                    FillAmount = _enemyView.FishScoreDictionary[_enemyView.EnemyFishType]
                });
                
                Destroy();
            }
        }
        
        public void Dispose()
        {
            _pool.Despawn(this);
        }
        
        public void Destroy()
        {
            _enemyDestroyHandler.Destroy();
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<GetPlayerFishTypeSignal>(OnGetPlayerFishType);
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        public class Factory : PlaceholderFactory<EnemyFacade>
        {
            
        }
    }
}