using System.Collections.Generic;
using BaseProject.Scripts.Core.Econom;

namespace BaseProject.Scripts.Core.EventBus
{
    public class ChangeCurrencySignal : IEvent
    {
        public readonly IReadOnlyList<CurrencyData> Rewards;

        public ChangeCurrencySignal(IReadOnlyList<CurrencyData> rewards)
        {
            Rewards = rewards;
        }
    }

    public class ClaimStashSignal : IEvent
    {
    }

    public class CurrencyChangedSignal : IEvent
    {
    }

    public class RewardWatchedSignal : IEvent
    {
        public readonly string AdPlaceId;

        public RewardWatchedSignal(string adPlaceId)
        {
            AdPlaceId = adPlaceId;
        }
    }

    public class GameStateChangedSignal : IEvent
    {
        public readonly GameState.GameState State;

        public GameStateChangedSignal(GameState.GameState state)
        {
            State = state;
        }
    }
}