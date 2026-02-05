using UnityEngine;

namespace BaseProject.Example.Scripts.Gameplay
{
    [CreateAssetMenu(fileName = "ClickOptionsConfig", menuName = "Scriptable Objects/ClickOptionsConfig")]
    public class ClickOptionsConfig : ScriptableObject
    {
        [SerializeField] private int _clicksToWin = 10;
        [SerializeField] private int _coinPerClick = 1;

        public int ClicksToWin => _clicksToWin;
        public int CoinPerClick => _coinPerClick;
        
    }
}
