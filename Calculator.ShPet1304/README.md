# C# Console Calculator
A professional-grade CLI calculator built with C# and .NET. This project began as a foundational Microsoft tutorial and was extended into a multi-project solution featuring advanced mathematics, persistent logging, and a modern interactive interface.

## Features
- Interactive Menu: Utilizes Spectre.Console for a rich, user-friendly terminal experience with arrow-key navigation.

- Advanced Mathematics: Supports basic arithmetic plus Square Roots, Powers, and Trigonometric functions (Sin, Cos, Tan).

- Result History: Automatically stores calculations in a session history. Users can view previous results or "inject" a past result back into a new calculation.

- JSON Logging: Every operation is logged to a calculatorlog.json file using Newtonsoft.Json for audit and debugging purposes.

- Defensive Programming: Robust input validation using TryParse to handle edge cases and prevent crashes from invalid user input.

## Technical Implementation

### Architecture
The project is split into a Multi-Project Solution to maintain a clean separation of concerns:
- CalculatorLibrary: A class library containing the "Engine." It implements the Singleton Pattern to ensure a single, consistent state for history and logging across the app.
- CalculatorProgram: The UI layer that manages user flow and input orchestration.

### Design Patterns
- Singleton Pattern: Used for the Calculator instance to manage shared resources like the JSON writer and history list.
- State Management: Implemented logic to flag and store "reused" numbers, allowing for complex multi-step calculations.


## Requirements & Libraries
- .NET 8.0 SDK (or newer)
- Spectre.Console: For the interactive CLI.
- Newtonsoft.Json: For data persistence and logging.

## How to Run
1. Clone the repository.
2. Open the solution in Visual Studio or VS Code.
3. Run dotnet restore to install the dependencies.
4. Press F5 or run dotnet run --project Calculator to start the app.





##### This project represents my transition from IT Support to Application Development. It highlights my focus on building tools that are not only functional but also robust, logged, and easy for the end-user to navigate.
