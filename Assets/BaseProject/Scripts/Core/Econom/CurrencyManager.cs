using System;
using System.Collections.Generic;
using BaseProject.Scripts.Core.EventBus;

namespace BaseProject.Scripts.Core.Econom
{
    public class CurrencyManager : ICurrencyManager
    {
        private readonly ICurrencyStorage _currenciesStorage;
        private readonly CurrencyModel[] _currencies;
        private readonly IEventBus _eventBus;

        public CurrencyManager(IEventBus eventBus, ICurrencyStorage currenciesStorage, string[] currencyTypes)
        {
            _eventBus = eventBus;
            _currenciesStorage = currenciesStorage;

            if (currencyTypes.Length == 0)
                throw new ArgumentException("Currency types cannot be null or empty", nameof(currencyTypes));

            _currencies = new CurrencyModel[currencyTypes.Length];

            LoadData(currencyTypes);
            Subscribe();
        }

        public int GetAmount(string currencyType)
        {
            if (string.IsNullOrEmpty(currencyType))
                throw new ArgumentException("Currency type cannot be null or empty", nameof(currencyType));

            return GetCurrencyModel(currencyType).Amount;
        }

        public void AddCurrencies(IReadOnlyList<CurrencyData> currencies)
        {
            bool hasChanges = false;
            foreach (var currency in currencies)
            {
                if (TryAddCurrency(currency))
                    hasChanges = true;
            }

            if (hasChanges)
            {
                _currenciesStorage.Save(_currencies);
            }
        }

        public bool SpendCurrency(string currencyType, int amount)
        {
            if (string.IsNullOrEmpty(currencyType))
                return false;

            if (amount <= 0)
                return false;

            if (!CanSpend(currencyType, amount))
                return false;

            var currencyModel = GetCurrencyModel(currencyType);
            currencyModel.Amount = Math.Max(0, currencyModel.Amount - amount);

            _currenciesStorage.Save(_currencies);

            return true;
        }

        public bool CanSpend(string currencyType, int amount)
        {
            if (string.IsNullOrEmpty(currencyType))
                return false;

            if (amount <= 0)
                return false;

            try
            {
                var currencyModel = GetCurrencyModel(currencyType);
                return currencyModel.Amount >= amount;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        private bool TryAddCurrency(CurrencyData currencyData)
        {
            var currencyModel = GetCurrencyModel(currencyData.CurrencyType);
            int oldAmount = currencyModel.Amount;
            currencyModel.Amount = Math.Clamp(currencyModel.Amount + currencyData.Amount, 0, int.MaxValue);
            return oldAmount != currencyModel.Amount;
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
            _eventBus.Subscribe<ChangeCurrencySignal>(OnChangeCurrencySignal);
        }

        private void OnChangeCurrencySignal(ChangeCurrencySignal signal)
        {
            AddCurrencies(signal.Rewards);
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
}