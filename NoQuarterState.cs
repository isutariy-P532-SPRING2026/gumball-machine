namespace GumballMachine;

public class NoQuarterState(GumballMachineLogic machine) : IState
{
    public string InsertQuarter()
    {
        machine.SetState(machine.HasQuarterState);
        return "You inserted a quarter";
    }

    public string EjectQuarter() => "You haven't inserted a quarter";
    public string TurnCrank()    => "You turned but there's no quarter";
    public string Dispense()     => "You need to pay first";
}
