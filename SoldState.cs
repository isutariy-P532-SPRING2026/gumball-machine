namespace GumballMachine;

public class SoldState(GumballMachineLogic machine) : IState
{
    public string InsertQuarter() => "Please wait, we're already giving you a gumball";
    public string EjectQuarter()  => "Sorry, you already turned the crank";
    public string TurnCrank()     => "Turning twice doesn't get you another gumball";

    public string Dispense()
    {
        machine.ReleaseBall();

        if (machine.Count > 0)
        {
            machine.SetState(machine.NoQuarterState);
            return "A gumball comes rolling out the slot!";
        }

        machine.SetState(machine.SoldOutState);
        return "A gumball comes rolling out the slot!\nOops, out of gumballs!";
    }
}
