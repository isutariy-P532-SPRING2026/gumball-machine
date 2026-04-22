namespace GumballMachine;

public class WinnerState(GumballMachineLogic machine) : IState
{
    public string InsertQuarter() => "Please wait, we're already giving you a gumball";
    public string EjectQuarter()  => "Sorry, you already turned the crank";
    public string TurnCrank()     => "Turning twice doesn't get you another gumball";

    public string Dispense()
    {
        // Always release at least 1 gumball
        machine.ReleaseBall();

        if (machine.Count == 0)
        {
            machine.SetState(machine.SoldOutState);
            return machine.WonWithQuarter
                ? "A gumball comes rolling out! (Only 1 left, no bonus.)\nOops, out of gumballs!"
                : "A gumball comes rolling out -- free turn winner!\nOops, out of gumballs!";
        }

        if (machine.WonWithQuarter)
        {
            // Quarter was inserted: bonus second gumball
            machine.ReleaseBall();
            machine.SetState(machine.Count > 0 ? machine.NoQuarterState : machine.SoldOutState);
            return "Two gumballs come rolling out -- you're a winner!";
        }

        // Free turn: 1 gumball only, no quarter was inserted
        machine.SetState(machine.NoQuarterState);
        return "A gumball comes rolling out -- free turn winner!";
    }
}
