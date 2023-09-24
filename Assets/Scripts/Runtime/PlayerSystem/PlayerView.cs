using UnityEngine;
using Zenject;

namespace Runtime.PlayerSystem
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private Transform playerTransform;
        
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        
        private PlayerPhysicHandler _playerPhysicHandler;
        
        [Inject]
        public void Construct(PlayerPhysicHandler playerPhysicHandler)
        {
            _playerPhysicHandler = playerPhysicHandler;
        }

        public Transform PlayerTransform
        {
            get => playerTransform; 
        }

        public SpriteRenderer SpriteRenderer
        {
            get => spriteRenderer;
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
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            _playerPhysicHandler.OnTriggerEnter2D(other);
        }
    }
}