using System;
using Runtime.Signals;
using Zenject;

namespace Runtime.PlayerSystem
{
    public class PlayerRendererHandler : IInitializable, IDisposable
    {
        private readonly PlayerModel _playerV;
        
        private readonly SignalBus _signalBus;
        
        public PlayerRendererHandler(
            PlayerModel playerV,
            SignalBus signalBus)
        {
            _playerV = playerV;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<OnMouseLeftClickSignal>(UpdateFlip);
        }
        
        public void UpdateFlip(OnMouseLeftClickSignal signal)
        {
            _playerV.SpriteRenderer.flipY = signal.InputParams.MousePosition.x < _playerV.Position.x;
        }
        
        private void UnSubscribeEvents()
        {
            _signalBus.Unsubscribe<OnMouseLeftClickSignal>(UpdateFlip);
        }

        public void Dispose()
        {
            UnSubscribeEvents();
        }
    }
}