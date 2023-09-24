using System;
using Assets.Scripts.Runtime.Signals;
using Runtime.AudioSystem;
using Runtime.BaitSystem;
using Runtime.EnemySystem;
using Runtime.Main;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

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
            
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            
            Container.BindFactory<BaitFacade, BaitFacade.Factory>()
                .FromPoolableMemoryPool<BaitFacade, BaitFacadePool>(poolBinder => poolBinder
                    .WithInitialSize(5)
                    .FromSubContainerResolve()
                    .ByNewPrefabInstaller<BaitInstaller>(_settings.BaitFacadePrefab)
                    .UnderTransformGroup("Baits"));
            
            Container.BindFactory<EnemyFacade, EnemyFacade.Factory>()
                .FromPoolableMemoryPool<EnemyFacade, EnemyFacadePool>(poolBinder => poolBinder
                    .WithInitialSize(20)
                    .FromSubContainerResolve()
                    .ByNewPrefabInstaller<EnemyInstaller>(_settings.ChooseEnemyFacadePrefab)
                    .UnderTransformGroup("Enemies"));
            
            Container.Bind<AudioPlayer>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<BaitRegistry>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<EnemyRegistry>().AsSingle();
            
            GameSignalsInstaller.Install(Container);

            Container.Bind<PlayerSignals>().AsSingle();
            
        }
        
        
        [Serializable]
        public class Settings
        {
            public GameObject BaitFacadePrefab;
            public GameObject[] EnemyFacadePrefabs;
            
            public GameObject ChooseEnemyFacadePrefab(InjectContext context)
            {
                return EnemyFacadePrefabs[Random.Range(0, EnemyFacadePrefabs.Length)];
            }
        }
        
        public class BaitFacadePool : MonoPoolableMemoryPool<IMemoryPool, BaitFacade>
        {
        }
        
        public class EnemyFacadePool : MonoPoolableMemoryPool<IMemoryPool, EnemyFacade>
        {
        }
    }
}