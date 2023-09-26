using System;
using Runtime.Signals;
using Zenject;

namespace Runtime.PlayerSystem
{
    public class PlayerRendererHandler : IInitializable, IDisposable
    {
        private readonly PlayerView _playerViev;
        
        private readonly SignalBus _signalBus;
        
        public PlayerRendererHandler(
            PlayerView playerViev,
            SignalBus signalBus)
        {
            _playerViev = playerViev;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<MouseLeftClickSignal>(OnMouseLeftClick);
            _signalBus.Subscribe<EvolvePlayerSignal>(OnEvolvePlayer);
        }
        
        public void OnMouseLeftClick(MouseLeftClickSignal signal)
        {
            _playerViev.SpriteRenderer.flipY = signal.InputParams.MousePosition.x < _playerViev.Position.x;
        }

        public void OnEvolvePlayer(EvolvePlayerSignal signal)
        {
            _playerViev.SpriteRenderer.sprite = signal.Sprite;
        }
        
        private void UnSubscribeEvents()
        {
            _signalBus.Unsubscribe<MouseLeftClickSignal>(OnMouseLeftClick);
            _signalBus.Unsubscribe<EvolvePlayerSignal>(OnEvolvePlayer);
        }

        public void Dispose()
        {
            UnSubscribeEvents();
        }
    }
}