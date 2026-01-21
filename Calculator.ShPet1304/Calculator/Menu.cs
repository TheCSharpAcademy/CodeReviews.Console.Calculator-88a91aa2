using CalculatorLibrary;
using CalculatorProgram;
using Spectre.Console;

namespace CalculatorMenu
{
    public class Menu
    {
        private static Calculator calcLib = Calculator.Instance;
        private static CalculatorFunctions func = new CalculatorFunctions();

        public string ShowMainMenu()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("Calculator")
                    .Color(Color.Blue));

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]What would you like to do[/]?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                    .AddChoices(new[] {
                    "New Calculation", "View Result History", "Use Previous Result", "View Calculator Usage Count", "Clear History", "Exit"
                    }));
            return choice;
        }

        public string ShowOperationsMenu()
        {
            AnsiConsole.Clear();
            var op = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]What would you like to do[/]?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                    .AddChoices(new[] {
                    "addition", "subtraction","multiplication", "division", "Square Root", "Taking the power", "10x", "Trigonometric function (sin)", "Trigonometric function (cos)", "Trigonometric function (tan)"
                    }));
            return op;
        }

        public void ShowHistoryMenu()
        {
            AnsiConsole.Clear();
            List<double> options = calcLib.PreviousResult();
            if (options.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No history found.[/]");
                return;
            }
            double selectedNum = AnsiConsole.Prompt(
            new SelectionPrompt<double>()
                .Title("Select a [green]previous result[/]:")
                .AddChoices(options)
        );
            func.reusedNumber = selectedNum;
            func.reusedResult = true;
            func.Function();
        }
    }
}