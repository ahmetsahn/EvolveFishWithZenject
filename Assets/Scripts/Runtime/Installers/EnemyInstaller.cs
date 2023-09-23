using Runtime.EnemySystem;
using Zenject;

namespace Runtime.Installers
{
    public class EnemyInstaller : Installer<EnemyInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnemyDestroyHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyMoveHandler>().AsSingle();
        }
    }

}