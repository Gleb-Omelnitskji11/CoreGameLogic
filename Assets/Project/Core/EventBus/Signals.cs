using System.Collections.Generic;

public readonly struct ChangeCurrencySignal
{
    public readonly IReadOnlyList<CurrencyData> Rewards;

    public ChangeCurrencySignal(IReadOnlyList<CurrencyData> rewards)
    {
        Rewards = rewards;
    }
}

public readonly struct ChangeCurrencyGenericSignal<T>
{
    public readonly IReadOnlyList<T> Rewards;

    public ChangeCurrencyGenericSignal(IReadOnlyList<T> rewards)
    {
        Rewards = rewards;
    }
}

public readonly struct ClaimStashSignal
{
}

public readonly struct CurrencyChangedSignal
{
}

public readonly struct RewardWatchedSignal
{
    public readonly string AdPlaceId;

    public RewardWatchedSignal(string adPlaceId)
    {
        AdPlaceId = adPlaceId;
    }
}

public readonly struct GameStateChangedSignal
{
    public readonly GameState State;

    public GameStateChangedSignal(GameState state)
    {
        State = state;
    }
}