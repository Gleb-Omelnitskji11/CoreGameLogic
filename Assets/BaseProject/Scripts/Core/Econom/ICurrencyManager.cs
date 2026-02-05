using System.Collections.Generic;

namespace BaseProject.Scripts.Core.Econom
{
    public interface ICurrencyManager
    {
        int GetAmount(string currencyType);
        void AddCurrencies(IReadOnlyList<CurrencyData> currencies);
        bool SpendCurrency(string currencyType, int amount);
        bool CanSpend(string currencyType, int amount);
    }
}