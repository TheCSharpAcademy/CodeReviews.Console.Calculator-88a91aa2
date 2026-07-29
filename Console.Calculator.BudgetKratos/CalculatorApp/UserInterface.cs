using Spectre.Console;

using CalculatorLibrary;
using CalculatorLibrary.Models;
using static CalculatorApp.Enums.Menus;

namespace CalculatorApp
{
    internal class UserInterface
    {
        private Calculator calculator = new();
        internal void MainMenu()
        {
            bool endApp = false;

            (string operationChosen, bool isComplexOperation) operationChoiceTuple = (String.Empty, false);

            (double firstOperand, double secondOperand) operandsTuple = (double.NaN, double.NaN);

            Console.Clear();

            AnsiConsole.Write(new FigletText("Console Calculator").Centered().Color(Color.Red));

            while(!endApp)
            {
                MainMenuChoices mainLoopChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<MainMenuChoices>()
                        .WrapAround()
                        .Title("What would you like to do?")
                        .AddChoices(Enum.GetValues<MainMenuChoices>())
                );

                switch(mainLoopChoice)
                {
                    case MainMenuChoices.History:
                        DisplayHistory(calculator.GetHistory());
                        break;

                    case MainMenuChoices.Recall:
                        CalculateFromRecalled(calculator.GetHistory());
                        break;

                    case MainMenuChoices.MemoryClear:
                        ClearHistory(calculator.GetHistory());
                        break;

                    case MainMenuChoices.UsageCount:
                        ShowUsageCount(calculator.GetHistory());
                        break;

                    case MainMenuChoices.Quit:
                        endApp = true;
                        break;

                    default:
                        operationChoiceTuple = ChooseOperation();
                        operandsTuple = GetUserInput(operationChoiceTuple.isComplexOperation);
                        DisplayResult(CallPerformOperation(operandsTuple.firstOperand, operandsTuple.secondOperand, operationChoiceTuple.operationChosen));                        
                        break;
                }

                AnsiConsole.MarkupLine("Press any key...");
                Console.ReadKey();
                Console.Clear();
            }
        }     

        private (string, bool) ChooseOperation()
        {
            bool IsComplexOperation = false;

            List<PossibleOperations> singleOperandOperations = new()
            {
                PossibleOperations.Square,
                PossibleOperations.SquareRoot,
                PossibleOperations.TenX,
                PossibleOperations.Sine,
                PossibleOperations.Cosine,
                PossibleOperations.Tangent
            };

            PossibleOperations operationChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<PossibleOperations>()
                        .PageSize(15)
                        .WrapAround()
                        .Title("Which operation would you like to choose?")
                        .AddChoices(Enum.GetValues<PossibleOperations>())
                    );

            IsComplexOperation = operationChoice switch
            {
                PossibleOperations x when singleOperandOperations.Contains(x) => true,
                _ => IsComplexOperation
            };

            return (operationChoice.ToString(), IsComplexOperation);
        }

        private (double, double) GetUserInput(bool isComplexOperation)
        {
            double firstOperand = double.NaN;
            double secondOperand = double.NaN;

            firstOperand = calculator.ExtractOperandFromUserInput(AnsiConsole.Ask<string>("Type a number: "));

            if (!isComplexOperation) secondOperand = calculator.ExtractOperandFromUserInput(AnsiConsole.Ask<string>("Type another number: "));

            return (firstOperand, secondOperand);
            
        }

        public double CallPerformOperation(double firstOperand, double secondOperand, string operationChoice)
        {
            double result = double.NaN;

            try
            {
                result = calculator.PerformOperation(firstOperand, secondOperand, operationChoice);
            }
            catch (Exception e)
            {
                AnsiConsole.MarkupLine($"An exception occurred trying to do that math.\nDetails: {e.Message}");
            }
            
            return result;
        }

        private void DisplayResult(double result)
        {
            if (double.IsNaN(result))
                AnsiConsole.MarkupLine("This operation will result in a mathematical error.");

            else
                AnsiConsole.MarkupLine($"Your result: {result:0.##}");
        }

        private void DisplayHistory(List<Equation> history)
        {
            if (history.Count == 0)
            {
                Console.Clear();
                AnsiConsole.MarkupLine("No history to display.");
            }
            else
            {
                var table = new Table();

                table.AddColumns(["First Operand", "Second Operand", "Operation Performed", "Result"]);

                foreach (Equation equation in history)
                {
                    table.AddRow(equation.Operand1.ToString(), equation.Operand2.ToString(), equation.OperationPerformed, Math.Round(equation.Result, 3).ToString());
                }

                AnsiConsole.Write(table);
            }
        }

        private void ClearHistory(List<Equation> history)
        {
            if (history.Count != 0)
            {
                if (AnsiConsole.Confirm("All history will be cleared, proceed?"))
                    calculator.MemoryClear();

                else return;
            }

            else
                AnsiConsole.MarkupLine("No history yet.");
        }

        private void CalculateFromRecalled(List<Equation> equations)
        {
            (string operationChosen, bool isComplexOperation) operationChoiceTuple = (String.Empty, false);

            operationChoiceTuple = ChooseOperation();

            (double firstOperand, double secondOperand) operandsTuple = (double.NaN, double.NaN);

            operandsTuple.firstOperand = AnsiConsole.Prompt(
                new SelectionPrompt<double>()
                .Title("First Operand")
                .AddChoices(equations.Select(e => e.Operand1).Distinct().ToList())
            );

            if (!operationChoiceTuple.isComplexOperation)
            {
                operandsTuple.secondOperand = AnsiConsole.Prompt(
                new SelectionPrompt<double>()
                .Title("Second Operand")
                .AddChoices(equations.Select(e => e.Operand2).Distinct().ToList())
                );
            }

            DisplayResult(CallPerformOperation(operandsTuple.firstOperand, operandsTuple.secondOperand, operationChoiceTuple.operationChosen));
        }

        private void ShowUsageCount(List<Equation> equations) => AnsiConsole.MarkupLine($"Calculator has been used {equations.Count} time(s) in total.");
    }
}
