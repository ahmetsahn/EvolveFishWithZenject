using System;
using Runtime.PlayerSystem;
using UnityEngine;
using Zenject;

namespace Runtime.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private Settings settings;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerModel>().AsSingle()
                .WithArguments(settings.Transform, settings.SpriteRenderer);
            
            Container.BindInterfacesTo<PlayerMoveHandler>().AsSingle();
            Container.BindInterfacesTo<PlayerRendererHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPhysicHandler>().AsSingle();

        }
        
        [Serializable]
        public class Settings
        {
            public Transform Transform;
            
            public SpriteRenderer SpriteRenderer;
        }
    }
}