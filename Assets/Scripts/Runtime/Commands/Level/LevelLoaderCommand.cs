using UnityEngine;


namespace Runtime.Commands.Level
{
    public class LevelLoaderCommand
    {
        private readonly Transform _levelRoot;
        
        private const string LEVEL_PREFAB_PATH = "Prefabs/Levels/Level";

        public LevelLoaderCommand(ref Transform levelRoot)
        {
            _levelRoot = levelRoot;
        }

        public GameObject Execute(int levelIndex)
        {  
            var levelPrefab = Object.Instantiate(Resources.Load<GameObject>(LEVEL_PREFAB_PATH + levelIndex), _levelRoot);
            return levelPrefab;
        }
    }
}