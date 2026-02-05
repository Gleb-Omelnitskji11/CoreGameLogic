using System;
using BaseProject.Scripts.Core.Econom;
using BaseProject.Scripts.Core.EventBus;
using BaseProject.Scripts.Core.GameState;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public abstract class BaseGameplayController : IDisposable
    {
        protected readonly ICurrencyManager CurrencyManager;
        protected readonly IEventBus EventBus;
        protected readonly  GameStateManager GameStateManager;

        protected bool IsGameActive { get; private set; }

        protected BaseGameplayController(
            IEventBus eventBus,
            GameStateManager gameStateManager,
            ICurrencyManager currencyManager)
        {
            GameStateManager = gameStateManager;
            EventBus = eventBus;
            CurrencyManager = currencyManager;

            SubscribeToSignals();
        }

        public virtual void Dispose()
        {
            UnsubscribeFromSignals();
        }

        private void UnsubscribeFromSignals()
        {
            EventBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        private void SubscribeToSignals()
        {
            EventBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            Debug.Log($"{signal.State}");
            switch (signal.State)
            {
                case GameState.Init:
                    Initialize();
                    break;

                case GameState.Gameplay:
                    StartGame();
                    break;

                case GameState.End:
                    EndGame();
                    break;

                case GameState.Restart:
                    Restart();
                    break;
            }
        }

        private void Restart()
        {
            Cleanup();
            GameStateManager.TrySetState(GameState.Gameplay);
        }
        
        protected virtual void Initialize()
        {
        }

        protected virtual void StartGame()
        {
            IsGameActive = true;
        }

        protected virtual void EndGame()
        {
            IsGameActive = false;
        }

        protected virtual void Cleanup()
        {
            IsGameActive = false;
        }
    }
}