using Runtime.Enums;

namespace Runtime.Signals
{
    public struct OpenPanelSignal
    {
        public UIPanelTypes PanelType;
        public int PanelIndex;
    }
    
    public struct ClosePanelSignal
    {
        public int PanelIndex;
    }
    
    public struct CloseAllPanelsSignal
    {
        
    }
    
    
}