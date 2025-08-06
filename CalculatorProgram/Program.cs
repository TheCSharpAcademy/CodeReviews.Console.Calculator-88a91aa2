using CalculatorLibrary;
using CalculatorResults;
using Utilities;

namespace CalculatorProgram;
class Program
{
    static void Main(string[] args)
    {
        ResultPrinter resultPrinter = new ResultPrinter();
        Utility utility = new Utility();
        Calculator calculator = new Calculator();
        Helpers helpers = new Helpers();

        helpers.ClearConsoleColors();
        helpers.PrintHeader();

        List<string> history = [];

        Boolean keepRunning = true;
        while (keepRunning)
        {
            var (cleanNum1, cleanNum2) = utility.GetTwoNumbers();
            char op = utility.GetOperatorChoice();

            if (op == 'E')
            {
                helpers.PrintExitMessage();
                break;
            }

            if (op == 'H')
            {
                helpers.PrintHistory(history, calculator.usageCount);
                continue;
            }

            if (op == 'X')
            {
                history.Clear();
                Console.WriteLine("History cleared.\n");
                continue;
            }

            try
            {
                double result = calculator.DoOperation(cleanNum1, cleanNum2, op);

                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else
                {

                    resultPrinter.PrintResult(cleanNum1, cleanNum2, result, op);

                    string operationName = op switch
                    {
                        'A' => "+",
                        'S' => "-",
                        'M' => "*",
                        'D' => "/",
                        _ => "?"
                    };
                    history.Add($"{calculator.usageCount}: {cleanNum1} {operationName} {cleanNum2} = {result}");

                    helpers.PrintHistory(history, calculator.usageCount);
                }
            }
            catch (Exception ex)
            {
                helpers.PrintError($"Oh no! An exception occurred trying to do the math.\n - Details: {ex.Message}");
            }

            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            string? userInput = Console.ReadLine()?.Trim().ToLower();
            if (userInput == "n")
            {
                helpers.PrintExitMessage();
                helpers.PrintFooter();
                break;
            }
            Console.Clear();

        }
        calculator.Close();
    }
}