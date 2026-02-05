using System;

namespace BaseProject.Scripts.Core.Econom
{
    [Serializable]
    public class CurrencyModel
    {
        public string CurrencyType;
        public int Amount;

        public CurrencyModel(string currencyType, int amount)
        {
            CurrencyType = currencyType;
            Amount = amount;
        }
    }
}