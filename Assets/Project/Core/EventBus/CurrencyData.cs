public class CurrencyData
{
    private string _currencyType;
    private int _amount;

    public CurrencyData(string currencyType, int amount)
    {
        _currencyType = currencyType;
        _amount = amount;
    }

    public CurrencyData()
    {
    }

    public string CurrencyType => _currencyType;
    public int Amount => _amount;
}