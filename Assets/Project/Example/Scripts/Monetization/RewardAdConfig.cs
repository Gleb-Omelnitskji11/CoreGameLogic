using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardAdConfig", menuName = "Scriptable Objects/RewardAdConfig")]
public class RewardAdConfig : ScriptableObject, IRewardedAdsHolder<RewardData>
{
    [SerializeField] private AdReward[] _adRewards = new AdReward[0];

    public IReadOnlyList<RewardData> GetReward(string adPlaceStr)
    {
        if (Enum.TryParse(adPlaceStr, ignoreCase: true, out AdPlaceId adPlaceId))
        {
            return GetReward(adPlaceId);
        }

        throw new KeyNotFoundException($"Wrong AdPlaceStr {adPlaceStr} for RewardAdConfig");
    }

    private IReadOnlyList<RewardData> GetReward(AdPlaceId adPlaceId)
    {
        foreach (var adReward in _adRewards)
        {
            if (adReward.AdPlaceId == adPlaceId)
                return adReward.Rewards;
        }

        throw new KeyNotFoundException($"Reward AdPlaceID {adPlaceId} not found in RewardAdConfig");
    }
}