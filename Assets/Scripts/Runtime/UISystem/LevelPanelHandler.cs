using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Enums;
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

        [SerializeField] private Image[] stageImages;
        
        [SerializeField] private List<Image> fishIcons;
        
        private int _stageIndex;
        private int _scoreValue;
        private int _levelIndex;

        private SignalBus _signalBus;
        
        private Settings[] _settings;
        
        private const string LEVEL_TEXT = "Level : ";
        private const string SCORE_TEXT = "Score : ";
        
        
        [Inject]
        public void Construct(SignalBus signalBus, Settings[] settings)
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
            _signalBus.Subscribe<LevelStartSignal>(OnLevelStart);
            _signalBus.Subscribe<IncreaseScoreSignal>(OnIncreaseScore);
            _signalBus.Subscribe<UpdateStageImageFillAmountSignal>(OnUpdateStageImageFillAmount);
            _signalBus.Subscribe<ResetGameSignal>(OnResetGame);
        }
        
        private void OnLevelStart(LevelStartSignal signal)
        {
            _levelIndex = signal.LevelIndex;

            var levelValue = _levelIndex + 1;
            
            levelText.text = LEVEL_TEXT + levelValue;
            scoreText.text = SCORE_TEXT + _scoreValue;

            for (int i = 0; i < fishIcons.Count; i++)
            {
                fishIcons[i].sprite = _settings[_levelIndex].FishIcons[i];
            }
            
            _signalBus.Fire(new GetPlayerFishTypeSignal()
            {
                FishType = _settings[_levelIndex].PlayerTypes[_stageIndex]
            });
        }
        
        private void OnIncreaseScore(IncreaseScoreSignal signal)
        {
            _scoreValue += signal.ScoreValue;
            scoreText.text = SCORE_TEXT + _scoreValue;
        }
        
        private void OnUpdateStageImageFillAmount(UpdateStageImageFillAmountSignal signal)
        {
            stageImages[_stageIndex].fillAmount += 
                signal.FillAmount / _settings[_levelIndex].StageValues[_stageIndex];

            if (!(stageImages[_stageIndex].fillAmount >= 1)) return;
            
            if (_settings[_levelIndex].StageValues.Count() - 1 == _stageIndex)
            {
                _signalBus.Fire(new ChangeGameStatesSignal
                {
                    GameStates = GameStates.Win
                });
                    
                return;
            }

            _signalBus.Fire(new EvolvePlayerSignal
            {
                Sprite = _settings[_levelIndex].FishEvolveSprites[_stageIndex]
            });
            
            _stageIndex++;
            
            _signalBus.Fire(new GetPlayerFishTypeSignal()
            {
                FishType = _settings[_levelIndex].PlayerTypes[_stageIndex]
            });
            
            Debug.Log(_settings[_levelIndex].PlayerTypes[_stageIndex]);
        }
        
        private void OnResetGame()
        {
            _stageIndex = 0;
            _scoreValue = 0;
            
            scoreText.text = SCORE_TEXT + _scoreValue;
            
            _signalBus.Fire(new GetPlayerFishTypeSignal()
            {
                FishType = _settings[_levelIndex].PlayerTypes[_stageIndex]
            });
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<LevelStartSignal>(OnLevelStart);
            _signalBus.Unsubscribe<IncreaseScoreSignal>(OnIncreaseScore);
            _signalBus.Unsubscribe<UpdateStageImageFillAmountSignal>(OnUpdateStageImageFillAmount);
            _signalBus.Unsubscribe<ResetGameSignal>(OnResetGame);
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        [Serializable]
        public struct Settings
        {
            public int[] StageValues;
            public FishType[] PlayerTypes;
            public Sprite[] FishEvolveSprites;
            public Sprite[] FishIcons;
        }
    }
}