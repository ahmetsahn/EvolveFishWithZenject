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
        }

        private void CoreGameSignals()
        {
            Container.DeclareSignal<OnChangeGameStatesSignal>();
            Container.DeclareSignal<OnResetGameSignal>();
        }
        
        private void CoreUISignals()
        {
            Container.DeclareSignal<OnOpenPanelSignal>();
            Container.DeclareSignal<OnClosePanelSignal>();
            Container.DeclareSignal<OnCloseAllPanelsSignal>();
        }

        private void UISignals()
        {
            Container.DeclareSignal<OnSetNewLevelValueSignal>();
            Container.DeclareSignal<OnIncreaseScoreSignal>();
            Container.DeclareSignal<OnUpdateStageImageFillAmountSignal>();
            Container.DeclareSignal<OnNextLevelButtonClickSignal>();
            Container.DeclareSignal<OnRestartLevelButtonClickSignal>();
            Container.DeclareSignal<OnQuitButtonClickSignal>();
        }

        private void LevelSignals()
        {
            Container.DeclareSignal<OnLevelStartSignal>();
            Container.DeclareSignal<OnUpdateStageIndexSignal>();
            Container.DeclareSignal<OnLevelDestroySignal>();
        }

        private void InputSignals()
        {
            Container.DeclareSignal<OnMouseLeftClickSignal>();
        }
    }
}