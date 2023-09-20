using System;
using UnityEngine;
using Zenject;

namespace Runtime.PlayerSystem
{
    public class PlayerFacade : MonoBehaviour
    {
        private PlayerModel _playerV;
        
        private PlayerPhysicHandler _playerPhysicHandler;
        
        [Inject]
        public void Construct(
            PlayerModel playerV, 
            PlayerPhysicHandler playerPhysicHandler)
            
        {
            _playerV = playerV;
            _playerPhysicHandler = playerPhysicHandler;
        }
        
        public bool IsDead
        {
            get => _playerV.IsDead;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _playerPhysicHandler.OnTriggerEnter2D(other);
        }
    }
}