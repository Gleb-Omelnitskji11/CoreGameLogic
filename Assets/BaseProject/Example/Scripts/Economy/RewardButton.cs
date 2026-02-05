using System;
using System.Threading;
using System.Threading.Tasks;
using BaseProject.Example.Scripts.Monetization;
using BaseProject.Scripts.Core.EventBus;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BaseProject.Example.Scripts.Economy
{
    public class RewardButton : MonoBehaviour
    {
        [SerializeField] private AdPlaceId _adPlaceId;
        [SerializeField] private Button _button;
        private IEventBus _eventBus;
        private CancellationTokenSource _currencyChangedCts;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Start()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            CancelCurrencyChanged();
        }

        private void OnDestroy()
        {
            CancelCurrencyChanged();
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            SetButtonInteractable(false);

            //Show Ad
            RewardWatchedSignal rewardWatchedSignal = new RewardWatchedSignal(_adPlaceId.ToString());
            _eventBus.Publish<RewardWatchedSignal>(rewardWatchedSignal);
            ScheduleCurrencyChanged();
        }

        private void SetButtonInteractable(bool interactable)
        {
            _button.interactable = interactable;
        }

        private void ScheduleCurrencyChanged()
        {
            CancelCurrencyChanged();

            _currencyChangedCts = new CancellationTokenSource();
            FireCurrencyChangedDelayedAsync(_currencyChangedCts.Token);
        }

        private async Task FireCurrencyChangedDelayedAsync(CancellationToken token)
        {
            try
            {
                await CurrencyAnimation.TestAnimate();
                _eventBus.Publish<CurrencyChangedSignal>(new CurrencyChangedSignal());
                SetButtonInteractable(true);
            }
            catch (TaskCanceledException)
            {
                // Expected when cancelled
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error in RewardButton animation: {ex.Message}");
                SetButtonInteractable(true);
            }
        }

        private void CancelCurrencyChanged()
        {
            if (_currencyChangedCts == null)
                return;

            _currencyChangedCts.Cancel();
            _currencyChangedCts.Dispose();
            _currencyChangedCts = null;
        }
    }
}