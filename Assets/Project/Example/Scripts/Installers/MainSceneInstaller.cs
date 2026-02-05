using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var adapter = StaticContext.Container.Resolve<RewardAdapter>();
    }
}