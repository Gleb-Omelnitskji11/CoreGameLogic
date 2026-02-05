using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public class SpendButton : MonoBehaviour
{
    [SerializeField] private RewardData _spendable;
    [SerializeField] private Button _button;
    private SignalBus _signalBus;
    private ICurrencyManager _currencyManager;

    [Inject]
    public void Construct(SignalBus signalBus, ICurrencyManager currencyManager)
    {
        _currencyManager = currencyManager;
        _signalBus = signalBus;
    }

    private void Start()
    {
        UpdateInteractable();
        Subscribe();
    }

    private void UpdateInteractable()
    {
        int amount = _currencyManager.GetAmount(_spendable.CurrencyType.ToString());
        bool isAvailable = amount >= _spendable.Amount;
        _button.interactable = isAvailable;
    }

    private void Subscribe()
    {
        _button.onClick.AddListener(OnClick);
        _signalBus.Subscribe<CurrencyChangedSignal>(UpdateInteractable);
    }
    
    private void OnClick()
    {
        CurrencyData currencyData = Utils.ConvertToCurrencyData(_spendable);
        IReadOnlyList<CurrencyData> reward = new[] { currencyData };
        ChangeCurrencySignal changeRewardSignal = new ChangeCurrencySignal(reward);
        _signalBus.Fire<ChangeCurrencySignal>(changeRewardSignal);
        _signalBus.Fire<CurrencyChangedSignal>();
    }
    
}