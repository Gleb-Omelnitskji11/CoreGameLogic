using BaseProject.Example.Scripts.Monetization;
using BaseProject.Scripts.Core.Econom;
using BaseProject.Scripts.Core.EventBus;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BaseProject.Example.Scripts.Economy
{
    public class SpendButton : MonoBehaviour
    {
        [SerializeField] private RewardData _spendable;
        [SerializeField] private Button _button;
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
            UpdateInteractable();
            Subscribe();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
            _eventBus.Unsubscribe<CurrencyChangedSignal>(OnCurrencyChanged);
        }

        private void OnCurrencyChanged(CurrencyChangedSignal signal)
        {
            UpdateInteractable();
        }

        private void UpdateInteractable()
        {
            string currencyType = _spendable.CurrencyType.ToString();
            bool isAvailable = _currencyManager.CanSpend(currencyType, _spendable.Amount);
            _button.interactable = isAvailable;
        }

        private void Subscribe()
        {
            if (_button != null)
                _button.onClick.AddListener(OnClick);

            _eventBus.Subscribe<CurrencyChangedSignal>(OnCurrencyChanged);
        }

        private void OnClick()
        {
            if (_spendable == null)
                return;

            string currencyType = _spendable.CurrencyType.ToString();
            bool success = _currencyManager.SpendCurrency(currencyType, _spendable.Amount);

            if (!success)
            {
                Debug.LogWarning($"Failed to spend {_spendable.Amount} {currencyType}");
                UpdateInteractable();
            }
        }
    }
}