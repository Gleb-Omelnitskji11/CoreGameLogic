using BaseProject.Example.Scripts.Monetization;
using BaseProject.Scripts.Core.Econom;

namespace BaseProject.Example.Scripts.Helper
{
    public static class Utils
    {
        public static CurrencyData ConvertToCurrencyData(RewardData rewardData)
        {
            return new CurrencyData(rewardData.CurrencyType.ToString(), rewardData.Amount);
        }
    }
}