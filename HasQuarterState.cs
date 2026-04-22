namespace GumballMachine;

public class HasQuarterState(GumballMachineLogic machine) : IState
{
    public string InsertQuarter() => "You can't insert another quarter";

    public string EjectQuarter()
    {
        machine.SetState(machine.NoQuarterState);
        return "Quarter returned";
    }

    public string TurnCrank()
    {
        machine.SetState(machine.SoldState);
        return machine.CurrentState.Dispense();
    }

    public string Dispense() => "No gumball dispensed";
}
