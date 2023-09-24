using UnityEngine;

namespace Runtime.Signals
{  
    public struct IncreaseScoreSignal
    {
        public int ScoreValue;
    }
    
    public struct UpdateStageImageFillAmountSignal
    {
        public float FillAmount;
    }
    
    public struct NextLevelButtonClickSignal
    {
        
    }
    
    public struct RestartLevelButtonClickSignal
    {
        
    }
    
    public struct QuitButtonClickSignal
    {
        
    }
}