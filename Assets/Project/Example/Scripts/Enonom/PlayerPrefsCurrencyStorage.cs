using System;
using UnityEngine;

public class PlayerPrefsCurrencyStorage : ICurrencyStorage
{
    private const string SaveKey = "Currencies";
    private CurrenciesModel _current;

    public CurrencyModel[] Load()
    {
        _current = GetOrCreateModel();
        return _current.Currencies;
    }

    private CurrenciesModel GetOrCreateModel()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            var json = PlayerPrefs.GetString(SaveKey);
            CurrenciesModel current = JsonUtility.FromJson<CurrenciesModel>(json);
            return current;
        }

        return new CurrenciesModel()
        {
            Currencies = GetDefaultData(),
        };
    }

    public void Save(CurrencyModel[] data)
    {
        CurrenciesModel currencies = new CurrenciesModel
        {
            Currencies = data
        };
        var json = JsonUtility.ToJson(currencies);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public void Clear()
    {
        PlayerPrefs.DeleteKey(SaveKey);
    }

    private CurrencyModel[] GetDefaultData()
    {
        return Array.Empty<CurrencyModel>();
    }

    [Serializable]
    public class CurrenciesModel
    {
        public CurrencyModel[] Currencies;
    }
}