using System.Collections.Generic;

public interface IRewardedAdsHolder<out T>
{
    public IReadOnlyList<T> GetReward(string adPlaceStr);
}