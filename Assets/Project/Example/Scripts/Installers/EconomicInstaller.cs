using System;
using Zenject;

public class EconomicInstaller : Installer<EconomicInstaller>
{
    public override void InstallBindings()
    {
        SignalBus signalBus = ProjectContext.Instance.Container.Resolve<SignalBus>();
        string[] currencyValues = Enum.GetNames(typeof(CurrencyType));
        ICurrencyStorage currencyStorage = new PlayerPrefsCurrencyStorage();
        ICurrencyManager currencyManager = new CurrencyManager(signalBus, currencyStorage, currencyValues);
        Container
            .Bind<ICurrencyManager>()
            .FromInstance(currencyManager)
            .AsSingle()
            .NonLazy();
    }
}