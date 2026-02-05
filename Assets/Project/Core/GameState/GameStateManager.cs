using Zenject;

public class GameStateManager
{
    private readonly SignalBus _signalBus;

    public GameState CurrentState { get; private set; } = GameState.Init;

    public GameStateManager(SignalBus eventBus)
    {
        _signalBus = eventBus;
    }

    public void SetState(GameState state)
    {
        CurrentState = state;
        _signalBus.Fire<GameStateChangedSignal>(new GameStateChangedSignal(state));
    }
}