using UnityEngine;

namespace Runtime.BaitSystem
{
    public class BaitView : MonoBehaviour
    {
        [SerializeField]
        private Transform baitTransform;
        
        public Vector3 Position
        {
            get => baitTransform.position;
            set => baitTransform.position = value;
        }
    }
}