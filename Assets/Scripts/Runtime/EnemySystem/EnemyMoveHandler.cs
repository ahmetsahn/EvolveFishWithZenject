using System;
using UnityEngine;
using Zenject;

namespace Runtime.EnemySystem
{
    public class EnemyMoveHandler : IFixedTickable
    {
        private readonly EnemyView _enemyView;
        private readonly EnemyMoveData _enemyMoveData;
        
        public EnemyMoveHandler(EnemyView enemyView, EnemyMoveData enemyMoveData)
        {
            _enemyView = enemyView;
            _enemyMoveData = enemyMoveData;
        }
        
        public void Move()
        {
            _enemyView.Position += _enemyView.Direction * _enemyMoveData.Speed * Time.deltaTime;
        }
        
        public void FixedTick()
        {
            Move();
        }
        
        [Serializable]
        public class EnemyMoveData
        {
            public float Speed;
        }
    }
}