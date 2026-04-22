namespace GumballMachine;

public class SoldOutState(GumballMachineLogic machine) : IState
{
    public string InsertQuarter() => "You can't insert a quarter, the machine is sold out";
    public string EjectQuarter()  => "You can't eject, you haven't inserted a quarter yet";
    public string TurnCrank()     => "You turned, but there are no gumballs";
    public string Dispense()      => "No gumball dispensed";
}
