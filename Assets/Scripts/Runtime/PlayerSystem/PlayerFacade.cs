using System;
using UnityEngine;
using Zenject;

namespace Runtime.PlayerSystem
{
    public class PlayerFacade : MonoBehaviour
    {
        private PlayerModel _playerModel;
        
        private PlayerPhysicHandler _playerPhysicHandler;
        
        [Inject]
        public void Construct(
            PlayerModel playerModel, 
            PlayerPhysicHandler playerPhysicHandler)
            
        {
            _playerModel = playerModel;
            _playerPhysicHandler = playerPhysicHandler;
        }
        
        public Vector3 Position
        {
            get => _playerModel.Position;
            set => _playerModel.Position = value;
        }
        
        public Transform PlayerTransform
        {
            get => _playerModel.PlayerTransform;
        }
        
        public bool IsDead
        {
            get => _playerModel.IsDead;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _playerPhysicHandler.OnTriggerEnter2D(other);
        }
    }
}