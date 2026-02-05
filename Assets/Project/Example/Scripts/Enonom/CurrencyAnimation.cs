using System.Threading.Tasks;

public static class CurrencyAnimation
{
    private static int Duration = 1000;
    public static async Task Animate()
    {
        await Task.Delay(Duration);
    }
}