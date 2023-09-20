using System.Collections.Generic;

namespace Runtime.EnemySystem
{
    public class EnemyRegistry
    {
        private readonly List<EnemyFacade> _enemies = new();

        public IEnumerable<EnemyFacade> Enemies => _enemies;

        public void AddBait(EnemyFacade enemy)
        {
            _enemies.Add(enemy);
        }

        public void RemoveBait(EnemyFacade enemy)
        {
            _enemies.Remove(enemy);
        }
    }
}