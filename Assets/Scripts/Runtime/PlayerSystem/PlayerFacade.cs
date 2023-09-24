using UnityEngine;
using Zenject;

namespace Runtime.PlayerSystem
{
    public class PlayerFacade : MonoBehaviour
    {
        private PlayerView _playerView;
        
        
        [Inject]
        public void Construct(
            PlayerView playerView)
            
        {
            _playerView = playerView;
        }
        
        public Vector3 Position
        {
            get => _playerView.Position;
            set => _playerView.Position = value;
        }
        
        public Transform PlayerTransform
        {
            get => _playerView.PlayerTransform;
        }
        
        public bool IsDead
        {
            get => _playerView.IsDead;
        }
    }
}