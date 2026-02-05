public interface ICurrencyManager
{
    int GetAmount(string currencyType);
    void AddCurrencies(ChangeCurrencySignal changeCurrencySignal);
}