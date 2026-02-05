using System.Threading.Tasks;

namespace BaseProject.Example.Scripts.Economy
{
    public static class CurrencyAnimation
    {
        private const int Duration = 1000;
        public static async Task TestAnimate()
        {
            await Task.Delay(Duration);
        }
    }
}