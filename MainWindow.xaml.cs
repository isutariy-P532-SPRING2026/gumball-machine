using System.Windows;

namespace GumballMachine;

public partial class MainWindow : Window
{
    private readonly GumballMachineLogic _machine = new(5);

    public MainWindow()
    {
        InitializeComponent();
        UpdateStatus();
        Log("Gumball Machine ready. Loaded with 5 gumballs.");
    }

    private void InsertQuarter_Click(object sender, RoutedEventArgs e)
    {
        Log(_machine.InsertQuarter());
        UpdateStatus();
    }

    private void EjectQuarter_Click(object sender, RoutedEventArgs e)
    {
        Log(_machine.EjectQuarter());
        UpdateStatus();
    }

    private void TurnCrank_Click(object sender, RoutedEventArgs e)
    {
        Log(_machine.TurnCrank());
        UpdateStatus();
    }

    private void Refill_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(RefillBox.Text, out int amount) && amount > 0)
        {
            Log(_machine.Refill(amount));
            UpdateStatus();
        }
        else
        {
            Log("Enter a valid refill amount.");
        }
    }

    private void UpdateStatus()
    {
        StateLabel.Text = _machine.GetStateName();
        CountLabel.Text = _machine.Count.ToString();
    }

    private void Log(string message)
    {
        LogBox.Text += $"> {message}\n";
        LogScroll.ScrollToEnd();
    }
}
