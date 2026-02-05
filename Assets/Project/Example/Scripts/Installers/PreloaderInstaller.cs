using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PreloaderInstaller : MonoInstaller<PreloaderInstaller>
{
    [SerializeField] private RewardAdConfig _rewardedAdsHolder;
    private DiContainer DiContainer => StaticContext.Container;


    public override void InstallBindings()
    {
        InstallStaticContext();
        GoNextScene();
    }

    protected virtual void InstallStaticContext()
    {
        DiContainer container = DiContainer;
        EconomicInstaller.Install(container);
        container.Bind<IRewardedAdsHolder<RewardData>>().FromInstance(_rewardedAdsHolder).AsSingle().NonLazy();
        container.Bind<IAnalyticsService>().To<DebugAnalyticsService>().AsSingle().NonLazy();
        container.Bind<GameStateManager>().AsSingle().NonLazy();
        container.BindInterfacesAndSelfTo<RewardAdapter>()
            .AsSingle()
            .NonLazy();
        container.Bind<CurrencyLogs>().AsSingle().NonLazy();
    }

    protected virtual void GoNextScene()
    {
        SceneManager.LoadScene(RouterConstants.MainScene);
    }
}