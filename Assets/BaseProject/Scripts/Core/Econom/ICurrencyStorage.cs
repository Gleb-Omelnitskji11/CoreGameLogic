namespace BaseProject.Scripts.Core.Econom
{
    public interface ICurrencyStorage
    {
        CurrencyModel[] Load();
        void Save(CurrencyModel[] data);
        void Clear();
    }
}