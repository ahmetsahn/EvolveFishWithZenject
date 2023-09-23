using System;
using Runtime.BaitSystem;
using Runtime.EnemySystem;
using Runtime.PlayerSystem;
using Runtime.UISystem;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Runtime.Installers
{
    [CreateAssetMenu(menuName = "Evolve Fish/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField]
        private GameInstaller.Settings gameInstaller;
        
        [SerializeField]
        private BaitSpawner.SpawnerData baitSpawnerData;
        
        [SerializeField]
        private EnemySpawner.EnemySpawnerData enemyEnemySpawnerData;
        
        [SerializeField]
        private LevelPanelHandler.Settings levelPanelSettings;
        
        [SerializeField]
        private BaitSettings baitSettings;
        
        [SerializeField]
        private PlayerSettings playerSettings;
        
        [SerializeField]
        private EnemySetting enemySettings;
        
        [Serializable]
        public class PlayerSettings
        {
            public PlayerMoveHandler.PlayerMovementData PlayerMovementData;
        }
            
        [Serializable]
        public class BaitSettings
        { 
            public BaitDestroyHandler.BaitDestroyData baitDestroyHandlerBaitDestroyData;
            public BaitTunable baitTunable;
        }

        [Serializable]
        public class EnemySetting
        {
            public EnemyDestroyHandler.EnemyDestroyData enemyDestroyHandlerEnemyDestroyData;
            public EnemyTunable EnemyTunable;
        }
        
        public override void InstallBindings()
        {
            Container.BindInstance(gameInstaller).IfNotBound();
            Container.BindInstance(baitSpawnerData).IfNotBound();
            Container.BindInstance(enemyEnemySpawnerData).IfNotBound();
            Container.BindInstance(levelPanelSettings).IfNotBound();
            
            Container.BindInstance(baitSettings.baitDestroyHandlerBaitDestroyData).IfNotBound();
            Container.BindInstance(baitSettings.baitTunable).IfNotBound();
            
            Container.BindInstance(playerSettings.PlayerMovementData).IfNotBound();
            
            Container.BindInstance(enemySettings.enemyDestroyHandlerEnemyDestroyData).IfNotBound();
            Container.BindInstance(enemySettings.EnemyTunable).IfNotBound();
        }
    }
}