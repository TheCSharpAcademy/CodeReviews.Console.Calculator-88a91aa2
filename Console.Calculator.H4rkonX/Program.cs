// Program.cs
using CalculatorLibrary;
using System.Text.RegularExpressions;

namespace CalculatorProgram;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        bool useResultsSelected = false;
        int numberOfUses = 0;
        List<string> historyOperation = new List<string>();
        List<double> historyResults = new List<double>();
        string? numInput1 = "";
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();
        while (!endApp)
        {
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            string? numInput2 = "";
            string operation = "";
            double result = 0;
            double cleanNum1 = 0;
            double cleanNum2 = 0;
            bool singleOperand = true;

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tpw - Taking the power");
            Console.WriteLine("\tsqr - Square root");
            Console.WriteLine("\t10 - Power of ten");
            Console.WriteLine("\tsin - Sine");
            Console.WriteLine("\tcos - Cosine");
            Console.WriteLine("\ttan - Tangent");
            Console.WriteLine();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("\tlog - Calculations during session");
            Console.WriteLine("\thist - Show history");
            Console.WriteLine("\tuse - Perform calculation with results");
            Console.WriteLine("\tdel - Delete history");
            Console.WriteLine("\texit - Exit calculator");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();
            string generalPattern = @"(?<=^|[^A-Za-z])(a|s|m|d|pw|sqr|10|sin|cos|tan|log|hist|use|del|exit)(?=$|[^A-Za-z])";
            string doubleOperandPattern = @"(?<=^|[^A-Za-z])(a|s|m|d|pw)(?=$|[^A-Za-z])";
            string commandsPattern = @"(?<=^|[^A-Za-z])(log|hist|use|del|exit)(?=$|[^A-Za-z])";

            // Validate input is not null, and matches the pattern
            if (op == null || !Regex.IsMatch(op, generalPattern))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else if (Regex.IsMatch(op, commandsPattern))
            {
                if (op == "use")
                {
                    op = "hist";
                    useResultsSelected = true;
                }
                switch (op)
                {
                    case "log":
                        string word = "calculation";
                        if (numberOfUses <= 0 || numberOfUses > 1)
                        {
                            word = "calculations";
                        }
                        Console.WriteLine();
                        Console.WriteLine($"You have made {numberOfUses} {word}!");
                        break;
                    case "hist":
                        int index = 1;
                        foreach (string element in historyOperation)
                        {
                            Console.WriteLine($"{index}. {element}");
                            index++;
                        }
                        if (useResultsSelected)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Select the result number you wanna use...");
                            int selected = 0;
                            while (true)
                            {
                                if (int.TryParse(Console.ReadLine(), out selected) && selected > 0 && selected <= historyResults.Count())
                                {
                                    numInput1 = Convert.ToString(historyResults[selected - 1]);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Please choose an available option!");
                                    Thread.Sleep(500);
                                    continue;
                                }
                            }
                        }
                        break;
                    case "del":
                        historyOperation.Clear();
                        historyResults.Clear();
                        Console.WriteLine("Deleting...");
                        Thread.Sleep(1000);
                        break;
                    case "exit":
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                }
            }
            else
            {
                if (!useResultsSelected)
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
                    double.TryParse(numInput1, out cleanNum1);
                    numInput1 = ""; // Reset when used
                }

                if (Regex.IsMatch(op, doubleOperandPattern))
                {
                    // Ask the user to type the second number.
                    Console.Write("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();
                    singleOperand = false;

                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput2 = Console.ReadLine();
                    }
                }
                try
                {
                    (result, operation) = calculator.DoOperation(cleanNum1, cleanNum2, op, singleOperand);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        numberOfUses += 1;
                        historyOperation.Add(operation);
                        historyResults.Add(result);
                        useResultsSelected = false;
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.

            Console.Clear();
        }
        calculator.Finish();
        return;
    }
}