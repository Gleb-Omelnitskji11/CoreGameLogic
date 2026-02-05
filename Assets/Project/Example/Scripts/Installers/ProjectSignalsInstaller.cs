using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Installers
{
    public class ProjectSignalsInstaller : MonoInstaller<ProjectSignalsInstaller>
    {
        public override void InstallBindings()
        {
            DeclareSignals();
        }

        protected virtual void DeclareSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<GameStateChangedSignal>();
            Container.DeclareSignal<RewardWatchedSignal>();
            Container.DeclareSignal<CurrencyChangedSignal>();
            Container.DeclareSignal<ChangeCurrencySignal>();
            Container.DeclareSignal<ChangeCurrencyGenericSignal<RewardData>>();
        }
    }
}