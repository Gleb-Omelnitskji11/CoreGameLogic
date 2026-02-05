using TMPro;
using UnityEngine;
using Zenject;

public class CurrencyCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _currencyCounter;
    [SerializeField] private CurrencyType _currencyType;
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
        UpdateCounter();
        Subscribe();
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<CurrencyChangedSignal>(UpdateCounter);
    }

    private void Subscribe()
    {
        _signalBus.Subscribe<CurrencyChangedSignal>(UpdateCounter);
    }

    private void UpdateCounter()
    {
        int amount = _currencyManager.GetAmount(_currencyType.ToString());
        _currencyCounter.text = amount.ToString();
    }
}