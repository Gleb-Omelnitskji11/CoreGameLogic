using System.Collections.Generic;

namespace BaseProject.Scripts.Core.Monetization
{
    public interface IRewardedAdsHolder<out T>
    {
        public IReadOnlyList<T> GetReward(string adPlaceStr);
    }
}