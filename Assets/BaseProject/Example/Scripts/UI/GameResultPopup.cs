using BaseProject.Scripts.Core.EventBus;
using BaseProject.Scripts.Core.GameState;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BaseProject.Example.Scripts.UI
{
    public class GameResultPopup : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private Button _restartButton;

        private const string WinText = "You Win!";
        private const string LoseText = "You Lose!";

        private IEventBus _eventBus;
        private GameStateManager _gameStateManager;

        [Inject]
        public void Construct(IEventBus eventBus, GameStateManager gameStateManager)
        {
            _eventBus = eventBus;
            _gameStateManager = gameStateManager;
        }

        private void Start()
        {
            _restartButton.onClick.AddListener(OnRestartClick);
            _eventBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        public void OnDestroy()
        {
            _restartButton.onClick.RemoveAllListeners();

            _eventBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            if (signal.State == GameState.End)
            {
                ShowPopup(true);
            }
        }

        private void ShowPopup(bool isWin)
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1;

            _resultText.text = isWin ? WinText : LoseText;
        }

        private void HidePopup()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0;
        }

        private void OnRestartClick()
        {
            _gameStateManager.TrySetState(GameState.Restart);
            HidePopup();
        }
    }
}