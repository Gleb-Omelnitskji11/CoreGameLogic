using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RewardAdapter
{
    private SignalBus _signalBus;
    private IRewardedAdsHolder<RewardData> _rewardAdsHolder;

    public RewardAdapter(IRewardedAdsHolder<RewardData> rewardAdsHolder)
    {
        _signalBus = ProjectContext.Instance.Container.Resolve<SignalBus>();
        _rewardAdsHolder = rewardAdsHolder;
        Subscribe();
    }

    private void Subscribe()
    {
        _signalBus.Subscribe<RewardWatchedSignal>(OnRewardAfterAd);
    }

    private void OnRewardAfterAd(RewardWatchedSignal rewardWatchedSignal)
    {
        IReadOnlyList<RewardData> rewards = _rewardAdsHolder.GetReward(rewardWatchedSignal.AdPlaceId);
        CurrencyData[] newRewards = ConvertToCurrenciesData(rewards);
        ChangeCurrencySignal changeRewardSignal = new ChangeCurrencySignal(newRewards);
        _signalBus.Fire<ChangeCurrencySignal>(changeRewardSignal);
    }

    private CurrencyData[] ConvertToCurrenciesData(IReadOnlyList<RewardData> rewardData)
    {
        CurrencyData[] newRewards = new CurrencyData[rewardData.Count];
        for (int i = 0; i < rewardData.Count; i++)
        {
            newRewards[i] = Utils.ConvertToCurrencyData(rewardData[i]);
        }

        return newRewards;
    }
}