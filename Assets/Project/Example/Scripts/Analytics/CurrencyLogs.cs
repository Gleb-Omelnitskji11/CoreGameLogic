using System;
using System.Collections.Generic;
using Zenject;

public class CurrencyLogs : IInitializable, IDisposable
{
    private SignalBus _signalBus;
    private ICurrencyManager _currencyManager;
    private IAnalyticsService _analyticsService;

    private const string AddCurrencyEventName = "AddCurrency";
    private const string RemoveCurrencyEventName = "RemoveCurrency";
    private const string AmountParameter = "Amount";

    [Inject]
    public void Construct(SignalBus signalBus, ICurrencyManager currencyManager, IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
        _currencyManager = currencyManager;
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<ChangeCurrencySignal>(OnLog);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<ChangeCurrencySignal>(OnLog);
    }

    private void OnLog(ChangeCurrencySignal signal)
    {
        foreach (var reward in signal.Rewards)
        {
            TradeLog(reward);
        }
    }

    private void TradeLog(CurrencyData currencyData)
    {
        string eventName = currencyData.Amount > 0 ? AddCurrencyEventName : RemoveCurrencyEventName;
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add(currencyData.CurrencyType, currencyData.CurrencyType);
        parameters.Add(AmountParameter, currencyData.Amount);
        _analyticsService.LogEvent(eventName, parameters);
    }
}