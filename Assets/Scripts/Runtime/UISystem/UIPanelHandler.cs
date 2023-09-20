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
            _signalBus.Subscribe<OnOpenPanelSignal>(OnOpenPanel);
            _signalBus.Subscribe<OnClosePanelSignal>(OnClosePanel);
            _signalBus.Subscribe<OnCloseAllPanelsSignal>(OnCloseAllPanels);
        }

        private void OnOpenPanel(OnOpenPanelSignal signal)
        {
            var panelType = signal.PanelType;
            var panelIndex = signal.PanelIndex;

            OnClosePanel(new OnClosePanelSignal { PanelIndex = panelIndex });
            _container.InstantiatePrefabResource(PANELS_PATH + panelType, layers[panelIndex]);
        }

        private void OnClosePanel(OnClosePanelSignal signal)
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
            _signalBus.Unsubscribe<OnOpenPanelSignal>(OnOpenPanel);
            _signalBus.Unsubscribe<OnClosePanelSignal>(OnClosePanel);
            _signalBus.Unsubscribe<OnCloseAllPanelsSignal>(OnCloseAllPanels);
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}