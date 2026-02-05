public static class Utils
{
    public static CurrencyData ConvertToCurrencyData(RewardData rewardData)
    {
        return new CurrencyData(rewardData.CurrencyType.ToString(), rewardData.Amount);
    }
}