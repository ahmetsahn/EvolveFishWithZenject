using System;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.PlayerSystem
{
    public class PlayerMoveHandler : ITickable, IInitializable, IDisposable
    {
        private readonly PlayerView _playerView;

        private readonly PlayerMovementData _movementData;
        
        private readonly SignalBus _signalBus;
        
        private Vector3 _mousePosition;
        public PlayerMoveHandler(
            PlayerView playerView,
            PlayerMovementData movementData,
            SignalBus signalBus)
        {
            _playerView = playerView;
            _movementData = movementData;
            _signalBus = signalBus;
        }

        private void Move()
        {
            var position = _playerView.Position;
            position = Vector3.Lerp(position, _mousePosition, _movementData.MoveSpeed * Time.fixedDeltaTime);
            _playerView.Position = position;
            _playerView.PlayerTransform.right = _mousePosition - position;
        }

        public void Tick()
        {
            Move();
        }

        public void Initialize()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<MouseLeftClickSignal>(UpdateMousePosition);
        }
        
        private void UpdateMousePosition(MouseLeftClickSignal signal)
        {
            _mousePosition = signal.InputParams.MousePosition;
        }
        
        private void UnSubscribeEvents()
        {
            _signalBus.Unsubscribe<MouseLeftClickSignal>(UpdateMousePosition);
        }

        public void Dispose()
        {
            UnSubscribeEvents();
        }
        
        [Serializable]
        public struct PlayerMovementData
        {
            public float MoveSpeed;
        }
    }
}