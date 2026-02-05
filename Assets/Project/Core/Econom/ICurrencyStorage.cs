public interface ICurrencyStorage
{
    CurrencyModel[] Load();
    void Save(CurrencyModel[] data);
    void Clear();
}