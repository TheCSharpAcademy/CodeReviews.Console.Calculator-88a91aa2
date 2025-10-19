using System;
using Spectre.Console;
using Calculator.Enums;

namespace Calculator;

internal class Calculator
{
    private readonly BasicOperations BasicOperations;
    private readonly ExpAndSqrOperations ExpAndSqrOperations;
    private readonly TrigonometricOperations TrigonometricOperations;

    private readonly LatestOperations latestOperations = new();
    private bool Open { get; set; } = true;

    public Calculator()
    {
        BasicOperations = new BasicOperations(latestOperations);
        ExpAndSqrOperations = new ExpAndSqrOperations(latestOperations);
        TrigonometricOperations = new TrigonometricOperations(latestOperations);
    }

    public void ViewMenu()
    {
        while (Open)
        {
            Console.Clear();

            AnsiConsole.Write(new Align(
                new Markup($"Number of operations made successfully!!!: [#ADA296]{Operations.NumberOperationMade}[/]"),
                HorizontalAlignment.Right
             ));

            AnsiConsole.Write(new Align(
                new Markup($"Last or Selected Result : [#ADA296]{(latestOperations.SelectedResult == null ? "no results to show" : latestOperations.SelectedResult)}[/]"),
                HorizontalAlignment.Left
             ));

            var menuOption = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOption>()
                .Title("Select the operation")
                .AddChoices(Enum.GetValues<MenuOption>()));

            switch (menuOption)
            {
                case MenuOption.Addition:
                    BasicOperations.Operation('+');
                    break;
                case MenuOption.Substraction:
                    BasicOperations.Operation('-');
                    break;
                case MenuOption.Multiplication:
                    BasicOperations.Operation('*');
                    break;
                case MenuOption.Division:
                    BasicOperations.Operation('/');
                    break;
                case MenuOption.Exponential:
                    ExpAndSqrOperations.Operation('e');
                    break;
                case MenuOption.SquareRoot:
                    ExpAndSqrOperations.Operation('s');
                    break;
                case MenuOption.Exponential10:
                    ExpAndSqrOperations.Operation('x');
                    break;
                case MenuOption.Sin:
                    TrigonometricOperations.Operation("sine");
                    break;
                case MenuOption.Cos:
                    TrigonometricOperations.Operation("cosine");
                    break;
                case MenuOption.Tan:
                    TrigonometricOperations.Operation("tangent");
                    break;
                case MenuOption.Results:
                    ResultsView();
                    break;
                case MenuOption.Exit:
                    Open = false;
                    break;
            }

        }

    }

    private void ResultsView()
    {
        bool inResultsView = true;

        while (inResultsView)
        {
            Console.Clear();

            latestOperations.ViewList();

            var menuOption = AnsiConsole.Prompt(
                new SelectionPrompt<ResultsMenu>()
                .Title("Select a choice")
                .AddChoices(Enum.GetValues<ResultsMenu>()));

            switch (menuOption)
            {
                case ResultsMenu.SelectResult:
                    latestOperations.SelectOperation();
                    break;
                case ResultsMenu.ClearList:
                    latestOperations.DeleteOperations();
                    break;
                case ResultsMenu.Exit:
                    inResultsView = false;
                    break;
            }

            AnsiConsole.Write(new Align(
                new Markup($"Last or Selected Result : [#ADA296]{(latestOperations.SelectedResult == null ? "no results to show" : latestOperations.SelectedResult)}[/]"),
                HorizontalAlignment.Left
             ));
        }
    }


}
