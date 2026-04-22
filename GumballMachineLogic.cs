namespace GumballMachine;

public class GumballMachineLogic
{
    public IState NoQuarterState  { get; }
    public IState HasQuarterState { get; }
    public IState SoldState       { get; }
    public IState SoldOutState    { get; }
    public IState WinnerState     { get; }

    public IState CurrentState  { get; private set; }
    public int    Count         { get; private set; }
    public bool   WonWithQuarter { get; private set; }

    private static readonly Random _rng = new();

    public GumballMachineLogic(int count)
    {
        NoQuarterState  = new NoQuarterState(this);
        HasQuarterState = new HasQuarterState(this);
        SoldState       = new SoldState(this);
        SoldOutState    = new SoldOutState(this);
        WinnerState     = new WinnerState(this);

        Count        = count;
        CurrentState = count > 0 ? NoQuarterState : SoldOutState;
    }

    public string InsertQuarter() => CurrentState.InsertQuarter();
    public string EjectQuarter()  => CurrentState.EjectQuarter();
    public string TurnCrank()
    {
        // Winner check is global — fires from NoQuarter (free turn) or HasQuarter (double gumball)
        bool eligibleForWin = Count > 0 &&
                              (CurrentState == NoQuarterState || CurrentState == HasQuarterState);

        if (eligibleForWin && _rng.Next(10) == 0)
        {
            WonWithQuarter = CurrentState == HasQuarterState;
            SetState(WinnerState);
            return "YOU'RE A WINNER! " + CurrentState.Dispense();
        }

        return CurrentState.TurnCrank();
    }

    public void SetState(IState state) => CurrentState = state;

    public void ReleaseBall() { if (Count > 0) Count--; }

    public void Refill(int amount)
    {
        Count += amount;
        if (CurrentState == SoldOutState && Count > 0)
            SetState(NoQuarterState);
    }

    public string GetStateName()
    {
        if (CurrentState == NoQuarterState)  return "No Quarter";
        if (CurrentState == HasQuarterState) return "Has Quarter";
        if (CurrentState == SoldState)       return "Gumball Sold";
        if (CurrentState == SoldOutState)    return "Sold Out";
        if (CurrentState == WinnerState)     return "Winner!";
        return "Unknown";
    }
}
