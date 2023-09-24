using System;
using Runtime.PlayerSystem;
using UnityEngine;
using Zenject;

namespace Runtime.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PlayerMoveHandler>().AsSingle();
            Container.BindInterfacesTo<PlayerRendererHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPhysicHandler>().AsSingle();
        }
    }
}