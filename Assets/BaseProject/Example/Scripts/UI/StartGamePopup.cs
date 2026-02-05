using BaseProject.Scripts.Core.GameState;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BaseProject.Example.Scripts.UI
{
    public class StartGamePopup : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private GameStateManager _gameStateManager;

        [Inject]
        public void Construct(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }

        private void Start()
        {
            _gameStateManager.TrySetState(GameState.Init);
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            _gameStateManager.TrySetState(GameState.Gameplay);
            TurnOff();
        }

        private void TurnOff()
        {
            gameObject.SetActive(false);
        }
    }
}