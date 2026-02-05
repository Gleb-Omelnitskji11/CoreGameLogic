using System;
using System.Collections.Generic;
using Zenject;

public class CurrencyManager : ICurrencyManager
{
    private readonly ICurrencyStorage _currenciesStorage;
    private readonly CurrencyModel[] _currencies;
    private readonly SignalBus _signalBus;

    public CurrencyManager(SignalBus signalBus, ICurrencyStorage currenciesStorage, string[] currencyTypes)
    {
        _signalBus = signalBus;
        _currenciesStorage = currenciesStorage;
        _currencies = new CurrencyModel[currencyTypes.Length];

        LoadData(currencyTypes);
        Subscribe();
    }

    public int GetAmount(string currencyType)
    {
        return GetCurrencyModel(currencyType).Amount;
    }

    public void AddCurrencies(ChangeCurrencySignal changeCurrencySignal)
    {
        foreach (var reward in changeCurrencySignal.Rewards)
        {
            AddCurrency(reward);
        }

        _currenciesStorage.Save(_currencies);
    }

    private void AddCurrency(CurrencyData currencyData)
    {
        var currencyModel = GetCurrencyModel(currencyData.CurrencyType);
        currencyModel.Amount = Math.Clamp(currencyModel.Amount + currencyData.Amount, 0, int.MaxValue);
    }

    private void LoadData(string[] currencyTypes)
    {
        CurrencyModel[] loaded = _currenciesStorage.Load();
        for (int i = 0; i < currencyTypes.Length; i++)
        {
            var type = currencyTypes[i];

            int amount = 0;
            for (int j = 0; j < loaded.Length; j++)
            {
                if (loaded[j].CurrencyType == type)
                {
                    amount = loaded[j].Amount;
                    break;
                }
            }

            _currencies[i] = new CurrencyModel(type, amount);
        }
    }


    private void Subscribe()
    {
        _signalBus.Subscribe<ChangeCurrencySignal>(AddCurrencies);
    }

    private CurrencyModel GetCurrencyModel(string currencyType)
    {
        foreach (var currency in _currencies)
        {
            if (currency.CurrencyType == currencyType)
                return currency;
        }

        throw new KeyNotFoundException($"Wrong currency type: {currencyType}");
    }
}