using System;
using Runtime.BaitSystem;
using Runtime.PlayerSystem;
using Runtime.UISystem;
using UnityEngine;
using Zenject;

namespace Runtime.Installers
{
    [CreateAssetMenu(menuName = "Evolve Fish/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField]
        private GameInstaller.Settings gameInstaller;
        
        [SerializeField]
        private BaitSpawner.Settings baitSpawnerData;
        
        [SerializeField]
        private LevelPanelHandler.Settings levelPanelSettings;
        
        [SerializeField]
        private BaitSettings baitSettings;
        
        [SerializeField]
        private PlayerSettings playerSettings;
        
        [Serializable]
        public class PlayerSettings
        {
            public PlayerMoveHandler.PlayerMovementData PlayerMovementData;
        }
            
        [Serializable]
        public class BaitSettings
        {
            public BaitDestroyHandler.Settings BaitDestroyHandlerSettings;
            public BaitTunable baitTunable;
        }
        
        public override void InstallBindings()
        {
            Container.BindInstance(gameInstaller).IfNotBound();
            Container.BindInstance(baitSpawnerData).IfNotBound();
            Container.BindInstance(levelPanelSettings).IfNotBound();
            
            Container.BindInstance(baitSettings.BaitDestroyHandlerSettings).IfNotBound();
            Container.BindInstance(baitSettings.baitTunable).IfNotBound();
            
            Container.BindInstance(playerSettings.PlayerMovementData).IfNotBound();
        }
    }
}