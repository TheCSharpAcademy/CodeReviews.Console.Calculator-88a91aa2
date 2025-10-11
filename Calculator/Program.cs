// Program.cs
using CalculatorLibrary;

namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            double? latestResult = null;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tr - Square root");
                Console.WriteLine("\tp - Taking the Power");
                Console.WriteLine("\tt - 10x");
                Console.WriteLine("\tx - Sine");
                Console.WriteLine("\ty - Cosine");
                Console.WriteLine("\tz - Tangent");
                Console.WriteLine("\th - History(total use count)");
                Console.WriteLine("\tc - Clear Result List");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !System.Text.RegularExpressions.Regex.IsMatch(op, "[a|s|m|d|r|p|t|x|y|z|h|c]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(op, "[h]"))
                {
                    Console.WriteLine($"The amount of times the calculator was used: {calculator.DisplayUsedCount()}");
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(op, "[c]"))
                {
                    calculator.ClearResultList();
                    Console.WriteLine($"Result list cleared");
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(op, "[r|t|x|y|z]"))
                {
                    string? numInput = "";
                    double result = 0;
                    double cleanNum = 0;

                    if (latestResult == null)
                    {
                        // Ask the user to type the first number.
                        Console.Write("Type a number, and then press Enter: ");
                        numInput = Console.ReadLine();

                        while (!double.TryParse(numInput, out cleanNum))
                        {
                            Console.Write("This is not valid input. Please enter an integer value: ");
                            numInput = Console.ReadLine();
                        }
                    }
                    else
                    {
                        cleanNum = (double)latestResult;
                        Console.WriteLine($"Using the latest result as the operand: {latestResult}");
                    }

                    try
                    {
                        result = calculator.DoOperation(cleanNum, op);
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
                }
                else
                {
                    // Declare variables and set to empty.
                    // Use Nullable types (with ?) to match type of System.Console.ReadLine
                    string? numInput1 = "";
                    string? numInput2 = "";
                    double result = 0;
                    double cleanNum1 = 0;
                    double cleanNum2 = 0;

                    if (latestResult == null)
                    {
                        // Ask the user to type the first number.
                        Console.Write("Type a number, and then press Enter: ");
                        numInput1 = Console.ReadLine();

                        while (!double.TryParse(numInput1, out cleanNum1))
                        {
                            Console.Write("This is not valid input. Please enter an integer value: ");
                            numInput1 = Console.ReadLine();
                        }
                    }
                    else
                    {
                        cleanNum1 = (double)latestResult;
                        Console.WriteLine($"Using the latest result as the operand: {latestResult}");
                    }

                    // Ask the user to type the second number.
                    Console.Write("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();

                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput2 = Console.ReadLine();
                    }

                    try
                    {
                        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
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
                }
                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or r to use the latest result as the first operand, or press any other key and Enter to continue: ");
                string? option = Console.ReadLine();
                if (option == "n") endApp = true;

                latestResult = null;
                if (option == "r")
                {
                    List<double> results = calculator.GetResultList();
                    if (results.Count > 0)
                    {
                        latestResult = results[^1];
                    }
                    else
                    {
                        Console.WriteLine("Result list is empty");
                    }
                }

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }
    }
}
