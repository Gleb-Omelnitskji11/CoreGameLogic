using BaseProject.Scripts.Core.Econom;
using BaseProject.Scripts.Core.EventBus;
using TMPro;
using UnityEngine;
using Zenject;

namespace BaseProject.Example.Scripts.Economy
{
    public class CurrencyCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currencyCounter;
        [SerializeField] private CurrencyType _currencyType;
        private IEventBus _eventBus;
        private ICurrencyManager _currencyManager;

        [Inject]
        public void Construct(IEventBus eventBus, ICurrencyManager currencyManager)
        {
            _currencyManager = currencyManager;
            _eventBus = eventBus;
        }

        private void Start()
        {
            UpdateCounter();
            Subscribe();
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<CurrencyChangedSignal>(OnCurrencyChangedSignal);
        }

        private void Subscribe()
        {
            _eventBus.Subscribe<CurrencyChangedSignal>(OnCurrencyChangedSignal);
        }
    
        private void OnCurrencyChangedSignal(CurrencyChangedSignal signal)
        {
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            int amount = _currencyManager.GetAmount(_currencyType.ToString());
            _currencyCounter.text = amount.ToString();
        }
    }
}