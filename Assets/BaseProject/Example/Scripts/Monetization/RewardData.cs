using System;
using BaseProject.Example.Scripts.Economy;
using UnityEngine;

namespace BaseProject.Example.Scripts.Monetization
{
    [Serializable]
    public class RewardData
    {
        [SerializeField] private CurrencyType _currencyType;
        [SerializeField] private int _amount;

        public RewardData(CurrencyType currencyType, int amount)
        {
            _currencyType = currencyType;
            _amount = amount;
        }

        public RewardData()
        {
        }

        public CurrencyType CurrencyType => _currencyType;
        public int Amount => _amount;
    }
}