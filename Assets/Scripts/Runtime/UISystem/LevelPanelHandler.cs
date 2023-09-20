using System;
using System.Collections.Generic;
using Runtime.Main;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.UISystem
{
    public class LevelPanelHandler : MonoBehaviour
    {
        [SerializeField] private Text levelText, scoreText;
        
        [SerializeField] private List<Image> stageImages = new();
        
        private int _stageIndex;
        private int _scoreValue;

        private SignalBus _signalBus;
        
        private Settings _settings;
        
        private const string LEVEL_TEXT = "Level : ";
        private const string SCORE_TEXT = "Score : ";
        
        [Inject]
        public void Construct(SignalBus signalBus, Settings settings)
        {
            _signalBus = signalBus;
            _settings = settings;
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<OnLevelStartSignal>(OnLevelStart);
            _signalBus.Subscribe<OnIncreaseScoreSignal>(OnIncreaseScore);
            _signalBus.Subscribe<OnUpdateStageImageFillAmountSignal>(OnUpdateStageImageFillAmount);
            _signalBus.Subscribe<OnUpdateStageIndexSignal>(OnUpdateStageIndex);
            _signalBus.Subscribe<OnResetGameSignal>(OnResetGame);
        }
        
        private void OnLevelStart(OnLevelStartSignal signal)
        {
            _stageIndex = 0;
            _scoreValue = 0;
            
            var levelValue = signal.LevelIndex + 1;
            
            levelText.text = LEVEL_TEXT + levelValue;
            scoreText.text = SCORE_TEXT + _scoreValue;
        }
        
        private void OnIncreaseScore(OnIncreaseScoreSignal signal)
        {
            _scoreValue += signal.ScoreValue;
            scoreText.text = SCORE_TEXT + _scoreValue;
        }
        
        private void OnUpdateStageImageFillAmount(OnUpdateStageImageFillAmountSignal signal)
        {
            stageImages[_stageIndex].fillAmount += 
                signal.FillAmount / _settings.stageValues[_stageIndex];

            if (!(stageImages[_stageIndex].fillAmount >= 1)) return;
            
            if (_settings.stageValues.Count - 1 == _stageIndex)
            {
                _signalBus.Fire(new OnChangeGameStatesSignal
                {
                    GameStates = GameStates.Win
                });
                    
                return;
            }
                
            _stageIndex++;
            
            _signalBus.Fire<OnUpdateStageIndexSignal>();
        }
        
        private void OnUpdateStageIndex(OnUpdateStageIndexSignal signal)
        {
            signal.StageIndex = _stageIndex;
        }
        
        private void OnResetGame()
        {
            _stageIndex = 0;
            _scoreValue = 0;
            
            scoreText.text = SCORE_TEXT + _scoreValue;
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<OnLevelStartSignal>(OnLevelStart);
            _signalBus.Unsubscribe<OnIncreaseScoreSignal>(OnIncreaseScore);
            _signalBus.Unsubscribe<OnUpdateStageImageFillAmountSignal>(OnUpdateStageImageFillAmount);
            _signalBus.Unsubscribe<OnUpdateStageIndexSignal>(OnUpdateStageIndex);
            _signalBus.Unsubscribe<OnResetGameSignal>(OnResetGame);
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        [Serializable]
        public struct Settings
        {
            public List<int> stageValues;
        }
    }
}