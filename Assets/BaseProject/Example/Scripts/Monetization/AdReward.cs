using System;
using System.Collections.Generic;
using UnityEngine;

namespace BaseProject.Example.Scripts.Monetization
{
    [Serializable]
    public class AdReward
    {
        [SerializeField] private AdPlaceId _adPlaceId;
        [SerializeField] private List<RewardData> _rewards = new List<RewardData>();

        public AdPlaceId AdPlaceId => _adPlaceId;
        public IReadOnlyList<RewardData> Rewards => _rewards;
    }
}