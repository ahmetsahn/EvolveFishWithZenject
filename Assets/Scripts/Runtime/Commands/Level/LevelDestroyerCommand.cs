using UnityEngine;

namespace Runtime.Commands.Level
{
    public class LevelDestroyerCommand
    {
        private readonly Transform _levelHolder;

        public LevelDestroyerCommand(ref Transform levelHolder)
        {
            _levelHolder = levelHolder;
        }

        public void Execute()
        {
            if (_levelHolder.transform.childCount <= 0) return;
            Object.Destroy(_levelHolder.transform.GetChild(0).gameObject);
        }
    }
}