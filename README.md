# Gumball Machine

A WPF desktop application built with C# and .NET that simulates a Gumball Machine using the **State Design Pattern**.

## Overview

The app models a real gumball machine with four states and transitions between them based on user actions.

### States

| State | Description |
|-------|-------------|
| No Quarter | Waiting for a quarter to be inserted |
| Has Quarter | Quarter inserted, ready to turn crank |
| Gumball Sold | Crank turned, dispensing gumball |
| Sold Out | No gumballs remaining |

### State Diagram

```
                      refill
              +--------------------+
              v                    |
        +----------+               |
        | Sold Out |               |
        +----------+               |
          ^     |                  |
gumballs=0|     | gumballs > 0    |
          |     v                  |
          |  +-------------+  inserts quarter  +-------------+
          |  |  No Quarter | ---------------> | Has Quarter |
          |  +-------------+ <--------------- +-------------+
          |                   ejects quarter        |
          |                                         | turns crank
          |                                         v
          |                                  +--------------+
          +--------------------------------- |  Gumball     |
                   dispense gumball          |  Sold        |
                                             +--------------+
```

> **Refill** is only valid from the **Sold Out** state — matching the state diagram.

## Actions

- **Insert Quarter** — Insert a coin to move into Has Quarter state
- **Eject Quarter** — Return the coin if not yet dispensing
- **Turn Crank** — Dispense a gumball and return to No Quarter (or Sold Out)
- **Refill** — Add gumballs back; only allowed when the machine is Sold Out

## Project Structure

```
GumballMachine/
├── GumballMachineLogic.cs   # State machine logic
├── MainWindow.xaml          # UI layout
├── MainWindow.xaml.cs       # UI code-behind
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
