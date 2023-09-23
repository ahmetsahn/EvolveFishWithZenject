using Runtime.BaitSystem;
using Zenject;
using Zenject.SpaceFighter;

namespace Runtime.Installers
{
    public class BaitInstaller : Installer<BaitInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BaitDestroyHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<BaitPhysicHandler>().AsSingle();
        }
    }
}