using CalculatorApp.CalculatorLibrary;

namespace CalculatorApp;

class CalculatorApp
{
    static void Main(string[] args)
    {
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        List<string> validOperators = ["a", "s", "m", "d", "sq", "p", "sin", "cos", "tan", "ten"];
        List<string> singleOperandOperators = ["sq", "sin", "cos", "tan", "ten"];
        List<string> calculatorCommands = ["c", "del", "h", "u"];
        Calculator clc = new();

        bool endApp = false;
        while (!endApp)
        {
            Console.WriteLine("Choose an operation from the following list:");

            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tten - 10x");
            Console.WriteLine("\tsq - Square Root");
            Console.WriteLine("\tp - Power");
            Console.WriteLine("\tsin - Sine");
            Console.WriteLine("\tcos - Cosine");
            Console.WriteLine("\ttan - Tangent");

            Console.WriteLine("");
            Console.WriteLine("------ CALCULATOR COMMANDS -----");
            Console.WriteLine("\tc - Show how many times calc was used");
            Console.WriteLine("\tdel - Delete history");
            Console.WriteLine("\th - Show history");
            Console.WriteLine("\tu - Use last result");
            Console.WriteLine("");

            string? op;
            while (true)
            {
                Console.Write("Your option? ");
                op = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(op))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                    continue;
                }

                op = op.Trim().ToLower();

                if (validOperators.Contains(op) || calculatorCommands.Contains(op))
                {
                    break;
                }

                Console.WriteLine("Error: Unrecognized input.");
            }

            if (validOperators.Contains(op))
            {
                // Declare variables and set to empty.
                string? numInput1 = "";
                double cleanNum1 = 0;
                double cleanNum2 = 0;

                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }

                if (!singleOperandOperators.Contains(op))
                {
                    Console.Write("Type another number, and then press Enter: ");
                    string? numInput2 = Console.ReadLine();
                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput2 = Console.ReadLine();
                    }
                }

                try
                {
                    double result = clc.DoOperation(op, cleanNum1, cleanNum2);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n")
                    endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            else if (calculatorCommands.Contains(op))
            {
                if (op == "u")
                {
                    try
                    {
                        double lastResult = clc.GetLastResult(clc.history);
                        double cleanNum2 = 0;

                        Console.WriteLine($"Using last result: {lastResult}");
                        Console.Write("Choose an operation from the list: ");

                        string? newOp = Console.ReadLine();
                        while (string.IsNullOrWhiteSpace(newOp) || !validOperators.Contains(newOp))
                        {
                            Console.WriteLine("Error: Unrecognized input.");
                            Console.Write("Choose an operation from the list: ");
                            newOp = Console.ReadLine();
                        }

                        if (!singleOperandOperators.Contains(newOp))
                        {
                            Console.Write("Type another number, and then press Enter: ");
                            string? numInput2 = Console.ReadLine();
                            while (!double.TryParse(numInput2, out cleanNum2))
                            {
                                Console.Write("This is not valid input. Please enter a numeric value: ");
                                numInput2 = Console.ReadLine();
                            }
                        }

                        double result = clc.DoOperation(newOp, lastResult, cleanNum2);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                }
                else
                {
                    clc.DoCommand(op);
                }
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }
        clc.Finish();
        return;
    }
}