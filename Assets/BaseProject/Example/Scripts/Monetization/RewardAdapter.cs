using System.Collections.Generic;
using BaseProject.Example.Scripts.Helper;
using BaseProject.Scripts.Core.Econom;
using BaseProject.Scripts.Core.EventBus;
using BaseProject.Scripts.Core.Monetization;

namespace BaseProject.Example.Scripts.Monetization
{
    public class RewardAdapter
    {
        private IEventBus _eventBus;
        private IRewardedAdsHolder<RewardData> _rewardAdsHolder;

        public RewardAdapter(IEventBus eventBus, IRewardedAdsHolder<RewardData> rewardAdsHolder)
        {
            _eventBus = eventBus;
            _rewardAdsHolder = rewardAdsHolder;
            Subscribe();
        }

        private void Subscribe()
        {
            _eventBus.Subscribe<RewardWatchedSignal>(OnRewardAfterAd);
        }

        private void OnRewardAfterAd(RewardWatchedSignal rewardWatchedSignal)
        {
            IReadOnlyList<RewardData> rewards = _rewardAdsHolder.GetReward(rewardWatchedSignal.AdPlaceId);
            CurrencyData[] newRewards = ConvertToCurrenciesData(rewards);
            ChangeCurrencySignal changeRewardSignal = new ChangeCurrencySignal(newRewards);
            _eventBus.Publish<ChangeCurrencySignal>(changeRewardSignal);
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
}