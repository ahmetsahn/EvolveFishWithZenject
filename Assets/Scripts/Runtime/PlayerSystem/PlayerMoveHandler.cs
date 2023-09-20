using System;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.PlayerSystem
{
    public class PlayerMoveHandler : IFixedTickable, IInitializable, IDisposable
    {
        private readonly PlayerModel _playerModel;

        private readonly PlayerMovementData _movementData;
        
        private readonly SignalBus _signalBus;
        
        private Vector3 _mousePosition;
        public PlayerMoveHandler(
            PlayerModel playerModel,
            PlayerMovementData movementData,
            SignalBus signalBus)
        {
            _playerModel = playerModel;
            _movementData = movementData;
            _signalBus = signalBus;
        }

        private void Move()
        {
            var position = _playerModel.Position;
            position = Vector3.Lerp(position, _mousePosition, _movementData.MoveSpeed * Time.fixedDeltaTime);
            _playerModel.Position = position;
            _playerModel.PlayerTransform.right = _mousePosition - position;
        }

        public void FixedTick()
        {
            Move();
        }

        public void Initialize()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<OnMouseLeftClickSignal>(UpdateMousePosition);
        }
        
        private void UpdateMousePosition(OnMouseLeftClickSignal signal)
        {
            _mousePosition = signal.InputParams.MousePosition;
        }
        
        private void UnSubscribeEvents()
        {
            _signalBus.Unsubscribe<OnMouseLeftClickSignal>(UpdateMousePosition);
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