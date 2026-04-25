using System.Text.RegularExpressions;

using CalculatorLibrary;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        int timesUsed = 0;
        List<double> history = new List<double>();
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();

        while (!endApp)
        {
            if (history.Count > 0)
            {
                Console.WriteLine("Press 'h' to view the history, or press 'c' to clear the history");
                string action = Console.ReadLine();
                if (action == "h")
                {
                    Console.WriteLine("History:\n");
                    foreach (var item in history)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("");
                }
                else if (action == "c")
                {
                    history.Clear();

                }
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n")
                {
                    endApp = true;
                    continue;
                }
            }
            
            Console.WriteLine($"Number of times the calculator have been used: {timesUsed}");
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;

            // Ask the user to type the first number.
            Console.Write("Type a number, or press 'l' to use last result, and then press Enter: ");
            numInput1 = Console.ReadLine();
            double cleanNum1 = 0;
            if (numInput1 == "l" && history.Count != 0)
            {
                cleanNum1 = history[history.Count - 1];
            }
            else
            {
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput2 = Console.ReadLine();
                }
            }

            // Ask the user to type the second number.
            Console.Write("Type another number, or press 'l' to use last result, and then press Enter: ");
            numInput2 = Console.ReadLine();
            double cleanNum2 = 0;
            if (numInput2 == "l" && history.Count != 0)
            {
                cleanNum2 = history[history.Count - 1];
            }
            else
            {
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput2 = Console.ReadLine();
                }
            }


                

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tsqrt - Square Root");
            Console.WriteLine("\tp - Power");
            Console.WriteLine("\t10x - 10x");
            Console.WriteLine("\tsin - Sinus");
            Console.WriteLine("\tcos - Cosinus");
            Console.WriteLine("\ttan - Tangent");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            

            // Validate input is not null, and matches the pattern
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d|sqrt|p|10x|sin|cos|tan]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    timesUsed++;
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                        history.Add(result);
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
            
            if (Console.ReadLine() == "n")
            {
                endApp = true;
            }

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        // Program.cs
        // Add call to close the JSON writer before return
        calculator.Finish();
        return;
    }
}