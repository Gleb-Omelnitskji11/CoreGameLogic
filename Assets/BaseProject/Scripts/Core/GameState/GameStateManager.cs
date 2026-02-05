using BaseProject.Scripts.Core.EventBus;
using UnityEngine;

namespace BaseProject.Scripts.Core.GameState
{
    public class GameStateManager
    {
        private readonly IEventBus _eventBus;

        public GameState CurrentState { get; private set; }

        public GameStateManager(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void TrySetState(GameState newState)
        {
            if (!CanTransition(CurrentState, newState))
            {
                Debug.LogWarning(
                    $"Invalid GameState transition: {CurrentState} → {newState}");
                return;
            }

            CurrentState = newState;
            _eventBus.Publish<GameStateChangedSignal>(new GameStateChangedSignal(newState));
        }

        private bool CanTransition(GameState from, GameState to)
        {
            return from switch
            {
                GameState.Init => to == GameState.Gameplay || to == GameState.Init,
                GameState.Gameplay => to == GameState.End,
                GameState.End => to == GameState.Restart,
                GameState.Restart => to == GameState.Gameplay,
                _ => false
            };
        }
    }
}