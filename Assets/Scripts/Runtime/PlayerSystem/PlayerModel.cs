using UnityEngine;

namespace Runtime.PlayerSystem
{
    public class PlayerModel
    {
        private readonly Transform _playerTransform;
        
        private readonly SpriteRenderer _spriteRenderer;
        
        private readonly PlayerPhysicHandler _playerPhysicHandler;
        public PlayerModel(
            Transform playerTransform, 
            SpriteRenderer spriteRenderer)
        {
            _playerTransform = playerTransform;
            _spriteRenderer = spriteRenderer;
        }

        public Transform PlayerTransform
        {
            get => _playerTransform; 
        }

        public SpriteRenderer SpriteRenderer
        {
            get => _spriteRenderer;
        }

        public Vector3 Position
        {
            get => PlayerTransform.position;
            set => PlayerTransform.position = value;
        }
        
        public bool IsDead
        {
            get;
            set;
        }
    }
}