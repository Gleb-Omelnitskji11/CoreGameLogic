using System.Collections.Generic;
using BaseProject.Example.Scripts.Economy;
using BaseProject.Example.Scripts.Helper;
using BaseProject.Scripts.Core.Econom;
using BaseProject.Scripts.Core.EventBus;
using BaseProject.Scripts.Core.GameState;
using BaseProject.Scripts.Core.Input;
using Game.Gameplay;
using Zenject;

namespace BaseProject.Example.Scripts.Gameplay
{
    public class ClickerGameplayController : BaseGameplayController, ITickable
    {
        private readonly IInputService _inputService;
        private TickRunner _tickRunner;
        private ClickOptionsConfig _clickOptionsConfig;

        private int _clicksToWin;
        private int _coinPerClick;

        public ClickerGameplayController(
            IEventBus eventBus,
            GameStateManager gameStateManager,
            ICurrencyManager currencyManager,
            IInputService inputService,
            TickRunner tickRunner,
            ClickOptionsConfig clickOptionsConfig)
            : base(eventBus, gameStateManager, currencyManager)
        {
            _clickOptionsConfig = clickOptionsConfig;
            _tickRunner = tickRunner;
            _inputService = inputService ?? throw new System.ArgumentNullException(nameof(inputService));
        }

        public void Tick()
        {
            if (!IsGameActive)
                return;

            bool inputPressed = _inputService.GetClick();

            if (inputPressed)
            {
                OnClick();
            }
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _tickRunner.Unregister(this);
        }

        protected override void StartGame()
        {
            base.StartGame();
            _tickRunner.Register(this);
            _clicksToWin = _clickOptionsConfig.ClicksToWin;
            _coinPerClick = _clickOptionsConfig.CoinPerClick;
        }

        private void OnClick()
        {
            var coinReward = new CurrencyData(CurrencyType.Coin.ToString(), _coinPerClick);
            CurrencyManager.AddCurrencies(new List<CurrencyData> { coinReward });
            EventBus.Publish<CurrencyChangedSignal>(new CurrencyChangedSignal());

            if (--_clicksToWin <= 0)
            {
                GameStateManager.TrySetState(GameState.End);
            }
        }
    }
}