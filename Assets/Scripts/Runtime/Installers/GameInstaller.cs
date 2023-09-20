using System;
using Runtime.AudioSystem;
using Runtime.BaitSystem;
using Runtime.Main;
using UnityEngine;
using Zenject;

namespace Runtime.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Inject]
        private Settings _settings = null;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<BaitSpawner>().AsSingle();
            
            Container.BindFactory<BaitFacade, BaitFacade.Factory>()
                .FromPoolableMemoryPool<BaitFacade, BaitFacadePool>(poolBinder => poolBinder
                    .WithInitialSize(5)
                    .FromSubContainerResolve()
                    .ByNewPrefabInstaller<BaitInstaller>(_settings.BaitFacadePrefab)
                    .UnderTransformGroup("Baits"));
            
            Container.Bind<AudioPlayer>().AsSingle();
            
            Container.Bind<BaitRegistry>().AsSingle();
            
            GameSignalsInstaller.Install(Container);
            
        }
        
        
        [Serializable]
        public class Settings
        {
            public GameObject BaitFacadePrefab;
        }
        
        public class BaitFacadePool : MonoPoolableMemoryPool<IMemoryPool, BaitFacade>
        {
        }
    }
}