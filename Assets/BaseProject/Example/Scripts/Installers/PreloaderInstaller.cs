using BaseProject.Example.Scripts.Analytics;
using BaseProject.Example.Scripts.Helper;
using BaseProject.Example.Scripts.Monetization;
using BaseProject.Scripts.Core.Analytics;
using BaseProject.Scripts.Core.EventBus;
using BaseProject.Scripts.Core.Input;
using BaseProject.Scripts.Core.Monetization;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace BaseProject.Example.Scripts.Installers
{
    public class PreloaderInstaller : MonoInstaller<PreloaderInstaller>
    {
        [SerializeField] private RewardAdConfig _rewardedAdsHolder;
        [SerializeField] private TickRunner _tickRunner;
    
        public override void InstallBindings()
        {
            InstallStaticContext();
            GoNextScene();
        }

        protected virtual void InstallStaticContext()
        {
            DiContainer container = StaticContext.Container;
            container.Bind<IEventBus>().To<EventBus>().AsSingle();
            EconomicInstaller.Install(container);
            container.Bind<IRewardedAdsHolder<RewardData>>().FromInstance(_rewardedAdsHolder).AsSingle();
            container.Bind<TickRunner>().FromInstance(_tickRunner).AsSingle();
            container.Bind<IAnalyticsService>().To<DebugAnalyticsService>().AsSingle();
            container.BindInterfacesAndSelfTo<RewardAdapter>().AsSingle();
            container.Bind<CurrencyLogs>().AsSingle();
            container.Bind<IInputService>().To<UnityInputService>().AsSingle();
        }

        protected virtual void GoNextScene()
        {
            SceneManager.LoadScene(Constants.MainScene);
        }
    }
}