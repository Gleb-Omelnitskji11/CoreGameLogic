using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public class RewardButton : MonoBehaviour
{
    [SerializeField] private AdPlaceId _adPlaceId;
    [SerializeField] private Button _button;
    private SignalBus _signalBus;
    private CancellationTokenSource _currencyChangedCts;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Start()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        CancelCurrencyChanged();
    }

    private void OnClick()
    {
        SetButtonInteractable(false);
        //Show Ad
        RewardWatchedSignal rewardWatchedSignal = new RewardWatchedSignal(_adPlaceId.ToString());
        _signalBus.Fire<RewardWatchedSignal>(rewardWatchedSignal);
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

    private async void FireCurrencyChangedDelayedAsync(CancellationToken token)
    {
        try
        {
            await CurrencyAnimation.Animate();
            _signalBus.Fire<CurrencyChangedSignal>();
            SetButtonInteractable(true);
        }
        catch (TaskCanceledException)
        {
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