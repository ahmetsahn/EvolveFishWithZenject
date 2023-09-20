using Runtime.Enums;

namespace Runtime.Signals
{
    public struct OnOpenPanelSignal
    {
        public UIPanelTypes PanelType;
        public int PanelIndex;
    }
    
    public struct OnClosePanelSignal
    {
        public int PanelIndex;
    }
    
    public struct OnCloseAllPanelsSignal
    {
        
    }
    
    public struct OnResetGameSignal
    {
        
    }
}