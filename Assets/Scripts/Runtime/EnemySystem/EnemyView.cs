using Runtime.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.EnemySystem
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField]
        private Transform enemyTransform;

        [SerializeField]
        private FishType enemyFishType;

        [SerializeField]
        private FishType[] eatableFishTypes;

        private Dictionary<FishType, int> _fishScoreDictionary = new();

        public Vector3 Position
        {
            get => enemyTransform.position;
            set => enemyTransform.position = value;
        }
        
        public Vector3 Direction
        {
            get => enemyTransform.right;
            set => enemyTransform.right = value;
        }

        public FishType EnemyFishType
        {
            get => enemyFishType;
            set => enemyFishType = value;
        }
        
        public FishType[] EatableFishTypes
        {
            get => eatableFishTypes;
            set => eatableFishTypes = value;
        }

        public Dictionary<FishType, int> FishScoreDictionary
        {
            get => _fishScoreDictionary;
            set => _fishScoreDictionary = value;
        }
    }
}