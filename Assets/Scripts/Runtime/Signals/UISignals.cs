namespace Runtime.Signals
{
    public struct OnSetNewLevelValueSignal
    {
        public int LevelValue;
    }
    
    public struct OnIncreaseScoreSignal
    {
        public int ScoreValue;
    }
    
    public struct OnUpdateStageImageFillAmountSignal
    {
        public int StageIndex;
        public float FillAmount;
    }
    
    public struct OnNextLevelButtonClickSignal
    {
        
    }
    
    public struct OnRestartLevelButtonClickSignal
    {
        
    }
    
    public struct OnQuitButtonClickSignal
    {
        
    }
    
}