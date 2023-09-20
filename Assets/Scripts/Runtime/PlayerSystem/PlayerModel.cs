using UnityEngine;

namespace Runtime.PlayerSystem
{
    public class PlayerModel
    {
        public PlayerModel(
            Transform playerTransform, 
            SpriteRenderer spriteRenderer)
        {
            PlayerTransform = playerTransform;
            SpriteRenderer = spriteRenderer;
        }
        
        public Transform PlayerTransform { get; }

        public SpriteRenderer SpriteRenderer { get; }

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