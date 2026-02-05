using System;
using BaseProject.Example.Scripts.Economy;
using BaseProject.Scripts.Core.Econom;
using Zenject;

namespace BaseProject.Example.Scripts.Installers
{
    public class EconomicInstaller : Installer<EconomicInstaller>
    {
        public override void InstallBindings()
        {
            ICurrencyStorage currencyStorage = new PlayerPrefsCurrencyStorage();
            Container.Bind<ICurrencyManager>()
                .To<CurrencyManager>()
                .AsSingle()
                .WithArguments(
                    currencyStorage,
                    Enum.GetNames(typeof(CurrencyType)))
                .NonLazy();
        }
    }
}