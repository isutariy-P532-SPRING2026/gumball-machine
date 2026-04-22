# Gumball Machine

A WPF desktop application built with C# and .NET that simulates a Gumball Machine using the **State Design Pattern**.

## Overview

Each machine state is its own class implementing `IState`. The machine holds references to all state instances and delegates every action to `CurrentState` — no conditionals in the machine itself.

## States

| State | Description |
|-------|-------------|
| `NoQuarterState` | Waiting for a quarter to be inserted |
| `HasQuarterState` | Quarter inserted, ready to turn crank |
| `SoldState` | Crank turned, dispensing 1 gumball |
| `WinnerState` | 10% chance winner — dispenses 1 or 2 gumballs depending on context |
| `SoldOutState` | No gumballs remaining, rejects all actions |

## State Diagram

```
                   inserts quarter
  +--------------+----------------> +--------------+
  |  NoQuarter   |                  | HasQuarter   |
  +--------------+ <--------------- +--------------+
        |           ejects quarter         |
        |                                  | turns crank (no win)
        | turns crank (wins, free turn)    v
        |                           +--------------+
        +-------> WinnerState <---- | HasQuarter   |  turns crank (wins)
        |         (1 or 2 balls)
        |
        | turns crank (no win)
        v
  "You turned but there's no quarter"

  SoldState / WinnerState
        |
        | gumballs > 0  -->  NoQuarterState
        | gumballs = 0  -->  SoldOutState
```

## Winner Logic

The winner check runs **globally in `GumballMachineLogic.TurnCrank()`**, before any state logic:

- Eligible states: `NoQuarterState` or `HasQuarterState` (machine must have gumballs)
- 10% chance (`Random.Next(10) == 0`)
- If the customer **had a quarter** when they won → `WinnerState` dispenses **2 gumballs**
- If the customer **had no quarter** when they won → `WinnerState` dispenses **1 free gumball**
- If only **1 gumball** is left when winning with a quarter → dispenses 1, transitions to `SoldOutState`

## Project Structure

```
GumballMachine/
├── IState.cs                  # State interface
├── NoQuarterState.cs
├── HasQuarterState.cs
├── SoldState.cs
├── WinnerState.cs
├── SoldOutState.cs
├── GumballMachineLogic.cs     # Holds all states, delegates to CurrentState
├── MainWindow.xaml            # UI layout
├── MainWindow.xaml.cs         # UI code-behind
├── App.xaml
├── App.xaml.cs
└── GumballMachine.csproj
```

## How to Run

```bash
dotnet run
```

## Requirements

- .NET 8.0 (Windows)
- Visual Studio 2022 or VS Code with C# extension
