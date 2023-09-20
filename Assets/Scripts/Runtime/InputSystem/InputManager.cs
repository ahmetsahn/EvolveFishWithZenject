using Runtime.Signals;
using Runtime.Keys;
using Runtime.Main;
using UnityEngine;
using Zenject;

namespace Runtime.InputSystem
{
    public class InputManager : MonoBehaviour
    {
        private Camera _mainCamera;
        
        private SignalBus _signalBus;
        
        private GameManager _gameManager;
        
        [Inject]
        public void Construct(SignalBus signalBus, GameManager gameManager)
        {
            _signalBus = signalBus;
            _gameManager = gameManager;
        }

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if(_gameManager.GameStates != GameStates.Playing) return;

            if (Input.touchCount <= 0) return;
            
            var mouseRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
            var mousePosition = mouseRay.origin;
            mousePosition.z = 0;
                
            _signalBus.Fire(new OnMouseLeftClickSignal()
            {
                InputParams = new InputParams()
                {
                    MousePosition = mousePosition
                }
            });
        }
    }
}