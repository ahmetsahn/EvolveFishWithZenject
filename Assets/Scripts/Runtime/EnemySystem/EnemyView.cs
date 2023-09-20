using UnityEngine;

namespace Runtime.EnemySystem
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField]
        private Transform enemyTransform;
        
        public Vector3 Position
        {
            get => enemyTransform.position;
            set => enemyTransform.position = value;
        }
    }
}