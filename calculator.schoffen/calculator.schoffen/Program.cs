using CalculatorLibrary;
using Spectre.Console;

namespace calculator.schoffen;

internal abstract class Program
{
    private static readonly Calculator Calculator = new();
    private static readonly List<Operation> LatestCalculations = [];

    private static void Main()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("----------------------------------");
            Console.WriteLine();

            // Main Menu Prompt
            var mainMenuOption = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                    .Title(Messages.Welcome.ToDisplay())
                    .UseConverter(o => o.ToDisplay())
                    .AddChoices(Enum.GetValues<MainMenuOptions>()));

            switch (mainMenuOption)
            {
                case MainMenuOptions.Calculate:
                    Console.Clear();
                    Calculate();
                    break;
                case MainMenuOptions.ShowLatestTransactions:
                    Console.Clear();
                    if (LatestCalculations.Count == 0)
                        Console.WriteLine("Nothing to show here.\n");
                    else
                    {
                        foreach (var latestCalculation in LatestCalculations)
                        {
                            Console.WriteLine(latestCalculation);
                        }
                    }

                    break;
                case MainMenuOptions.DeleteLatestTransactions:
                    Console.Clear();
                    if (LatestCalculations.Count == 0)
                        Console.WriteLine("Nothing to delete.\n");
                    else
                    {
                        LatestCalculations.Clear();
                        Console.WriteLine("All records have been deleted.\n");
                    }

                    break;
                case MainMenuOptions.Exit:
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private static void Calculate(double? previousResult = null)
    {
        while (true)
        {
            var operationOption = AnsiConsole.Prompt(new SelectionPrompt<OperationType>()
                .Title(Messages.SelectOperation.ToDisplay())
                .UseConverter(o => o.ToString())
                .AddChoices(Enum.GetValues<OperationType>()));

            var value1 = previousResult ?? AnsiConsole.Prompt(new TextPrompt<double>("First number: "));

            var operationNeedsSecondValue = operationOption is not (OperationType.SquareRoot or OperationType.TenX);

            var value2 = operationNeedsSecondValue ? AnsiConsole.Prompt(new TextPrompt<double>("Second number: ")) : 0;

            Operation calculation;

            switch (operationOption)
            {
                case OperationType.Add:
                    calculation = Calculator.Add(value1, value2);
                    break;
                case OperationType.Subtract:
                    calculation = Calculator.Subtract(value1, value2);
                    break;
                case OperationType.Multiply:
                    calculation = Calculator.Multiply(value1, value2);
                    break;
                case OperationType.Divide:
                    if (value2 != 0)
                    {
                        calculation = Calculator.Divide(value1, value2);
                        break;
                    }

                    const string divisionWithZeroErrorMessage = "You can't divide a number by 0";

                    Console.WriteLine(divisionWithZeroErrorMessage);

                    value2 = AnsiConsole.Prompt(new TextPrompt<double>("Second number: ").Validate(value => value != 0
                        ? ValidationResult.Success()
                        : ValidationResult.Error(divisionWithZeroErrorMessage)));

                    calculation = Calculator.Divide(value1, value2);
                    break;
                case OperationType.SquareRoot:
                    calculation = Calculator.SquareRoot(value1);
                    break;
                case OperationType.Power:
                    calculation = Calculator.Power(value1, value2);
                    break;
                case OperationType.TenX:
                    calculation = Calculator.TenX(value1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            LatestCalculations.Add(calculation);
            Console.WriteLine();
            Console.WriteLine(calculation.ToString());
            Console.WriteLine();

            var useResultInNewOperation = AnsiConsole.Confirm("Would you like to use this result in a new operation?");
            if (useResultInNewOperation)
            {
                previousResult = calculation.Result;
                continue;
            }

            break;
        }
    }
}