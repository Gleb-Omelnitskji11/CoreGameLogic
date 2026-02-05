namespace BaseProject.Scripts.Core.Econom
{
    public readonly struct CurrencyData
    {
        public readonly string CurrencyType;
        public readonly int Amount;

        public CurrencyData(string currencyType, int amount)
        {
            if (string.IsNullOrEmpty(currencyType))
                throw new System.ArgumentException("Currency type cannot be null or empty", nameof(currencyType));

            CurrencyType = currencyType;
            Amount = amount;
        }
    }
}