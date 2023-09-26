using Runtime.Signals;
using Zenject;

namespace Runtime.Installers
{
    public class GameSignalsInstaller : Installer<GameSignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            CoreGameSignals();
            CoreUISignals();
            UISignals();
            LevelSignals();
            InputSignals();
            PlayerSignals();
        }

        private void CoreGameSignals()
        {
            Container.DeclareSignal<ChangeGameStatesSignal>();
            Container.DeclareSignal<ResetGameSignal>();
        }
        
        private void CoreUISignals()
        {
            Container.DeclareSignal<OpenPanelSignal>();
            Container.DeclareSignal<ClosePanelSignal>();
            Container.DeclareSignal<CloseAllPanelsSignal>();
        }

        private void UISignals()
        {
            Container.DeclareSignal<IncreaseScoreSignal>();
            Container.DeclareSignal<UpdateStageImageFillAmountSignal>();
            Container.DeclareSignal<NextLevelButtonClickSignal>();
            Container.DeclareSignal<RestartLevelButtonClickSignal>();
            Container.DeclareSignal<QuitButtonClickSignal>();
        }

        private void LevelSignals()
        {
            Container.DeclareSignal<LevelStartSignal>();
            Container.DeclareSignal<LevelDestroySignal>();
            Container.DeclareSignal<NextLevelSignal>();
            Container.DeclareSignal<RestartLevelSignal>();
        }

        private void InputSignals()
        {
            Container.DeclareSignal<MouseLeftClickSignal>();
        }

        private void PlayerSignals()
        {
            Container.DeclareSignal<GetPlayerFishTypeSignal>();
            Container.DeclareSignal<EvolvePlayerSignal>();
        }
    }
}