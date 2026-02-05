using System;
using System.Collections.Generic;
using BaseProject.Scripts.Core.Analytics;
using BaseProject.Scripts.Core.Econom;
using BaseProject.Scripts.Core.EventBus;
using Zenject;

namespace BaseProject.Example.Scripts.Analytics
{
    public class CurrencyLogs : IInitializable, IDisposable
    {
        private IEventBus _eventBus;
        private ICurrencyManager _currencyManager;
        private IAnalyticsService _analyticsService;

        private const string AddCurrencyEventName = "AddCurrency";
        private const string RemoveCurrencyEventName = "RemoveCurrency";
        private const string AmountParameter = "Amount";

        [Inject]
        public void Construct(IEventBus eventBus, ICurrencyManager currencyManager, IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
            _currencyManager = currencyManager;
            _eventBus = eventBus;
        }

        public void Initialize()
        {
            _eventBus.Subscribe<ChangeCurrencySignal>(OnLog);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<ChangeCurrencySignal>(OnLog);
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
}