namespace GumballMachine;

public class GumballMachineLogic
{
    public const int SOLD_OUT = 0;
    public const int NO_QUARTER = 1;
    public const int HAS_QUARTER = 2;
    public const int SOLD = 3;

    public int State { get; private set; } = SOLD_OUT;
    public int Count { get; private set; }

    public GumballMachineLogic(int count)
    {
        Count = count;
        if (count > 0)
            State = NO_QUARTER;
    }

    public string InsertQuarter()
    {
        if (State == HAS_QUARTER)
            return "You can't insert another quarter";
        if (State == NO_QUARTER)
        {
            State = HAS_QUARTER;
            return "You inserted a quarter";
        }
        if (State == SOLD_OUT)
            return "You can't insert a quarter, the machine is sold out";
        return "Please wait, we're already giving you a gumball";
    }

    public string EjectQuarter()
    {
        if (State == HAS_QUARTER)
        {
            State = NO_QUARTER;
            return "Quarter returned";
        }
        if (State == NO_QUARTER)
            return "You haven't inserted a quarter";
        if (State == SOLD)
            return "Sorry, you already turned the crank";
        return "You can't eject, the machine is sold out";
    }

    public string TurnCrank()
    {
        if (State == SOLD)
            return "Turning twice doesn't get you another gumball";
        if (State == NO_QUARTER)
            return "You turned but there's no quarter";
        if (State == SOLD_OUT)
            return "You turned, but there are no gumballs";

        // HAS_QUARTER
        State = SOLD;
        return Dispense();
    }

    private string Dispense()
    {
        if (State == SOLD)
        {
            Count--;
            string msg = "A gumball comes rolling out the slot!";
            State = Count > 0 ? NO_QUARTER : SOLD_OUT;
            if (State == SOLD_OUT)
                msg += "\nOops, out of gumballs!";
            return msg;
        }
        return "No gumball dispensed";
    }

    public string GetStateName() => State switch
    {
        SOLD_OUT => "Sold Out",
        NO_QUARTER => "No Quarter",
        HAS_QUARTER => "Has Quarter",
        SOLD => "Gumball Sold",
        _ => "Unknown"
    };

    public string Refill(int amount)
    {
        if (State != SOLD_OUT)
            return "Machine doesn't need refilling yet";
        Count += amount;
        State = NO_QUARTER;
        return $"Machine refilled with {amount} gumball(s)!";
    }
}
