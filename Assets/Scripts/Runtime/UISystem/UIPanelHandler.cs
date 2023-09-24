using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.UISystem
{
    public class UIPanelHandler : MonoBehaviour
    {
        [SerializeField] private Transform[] layers;
        
        private SignalBus _signalBus;
        
        private DiContainer _container;
        
        private const string PANELS_PATH = "UIPanels/";
        
        
        [Inject]
        public void Construct(SignalBus signalBus, DiContainer container)
        {
            _signalBus = signalBus;
            _container = container;
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<OpenPanelSignal>(OnOpenPanel);
            _signalBus.Subscribe<ClosePanelSignal>(OnClosePanel);
            _signalBus.Subscribe<CloseAllPanelsSignal>(OnCloseAllPanels);
        }

        private void OnOpenPanel(OpenPanelSignal signal)
        {
            var panelType = signal.PanelType;
            var panelIndex = signal.PanelIndex;

            OnClosePanel(new ClosePanelSignal { PanelIndex = panelIndex });
            _container.InstantiatePrefabResource(PANELS_PATH + panelType, layers[panelIndex]);
        }

        private void OnClosePanel(ClosePanelSignal signal)
        {
            if (layers[signal.PanelIndex].childCount <= 0) return;
            Destroy(layers[signal.PanelIndex].GetChild(0).gameObject);
        }
        
        private void OnCloseAllPanels()
        {
            foreach (var layer in layers)
            {
                if (layer.childCount <= 0) return;
                Destroy(layer.GetChild(0).gameObject);
            }
        }

        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<OpenPanelSignal>(OnOpenPanel);
            _signalBus.Unsubscribe<ClosePanelSignal>(OnClosePanel);
            _signalBus.Unsubscribe<CloseAllPanelsSignal>(OnCloseAllPanels);
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}