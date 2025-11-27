using CalculatorLibrary;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        int calculationCount = 0;
        List<(string, double)> previousCalculationsList = new List<(string, double)>();
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");
        Calculator calculator = new Calculator();
        while (!endApp)
        {
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            previousCalculationsList = calculator.GetCalculations();
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;

            Console.Write("Welcome to the Calculator! Please type the one of the following commands:\nTo start calculating type: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("C");
            Console.ResetColor();
            Console.WriteLine(); 
            Console.Write("Previous calculations, type: ");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("H");
            Console.WriteLine();
            Console.ResetColor();
            string? userInput = Console.ReadLine();
            if (userInput != null)
            {
                if (userInput.ToUpper() == "H")
                {
                    Console.WriteLine("Previous Calculations:");
                    if (previousCalculationsList.Count == 0)
                    {
                        Console.WriteLine("No previous calculations found.");
                    }
                    else
                    {
                        bool _useFirstColour = true;
                        foreach (var calculation in previousCalculationsList) // Displays the previous calculations with alternating colours
                        {
                            ConsoleColor colourA = ConsoleColor.Blue;
                            ConsoleColor colourB = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = _useFirstColour ? colourA : colourB;
                            _useFirstColour = !_useFirstColour;
                            Console.WriteLine($"{calculation.Item1} {calculation.Item2}");
                        }
                        Console.ResetColor();
                    }
                    Console.WriteLine("------------------------\n");
                    Console.Write("If you would like to clear this history, type: ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Delete\n");
                    Console.ResetColor();
                    Console.Write("Otherwise, to continue with calculations - type: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("C");
                    Console.ResetColor();
                    userInput = Console.ReadLine();
                    if (userInput != null && userInput.ToUpper() == "DELETE")
                    {
                        calculator.ClearCalculations();
                        Console.WriteLine("Calculation history cleared.");
                    }
                    continue;
                }
                else if (userInput.ToUpper() != "C")
                {
                    Console.WriteLine("Error: Unrecognized input.");
                    Console.WriteLine("------------------------\n");
                    continue;
                }
            }

            // Ask the user to type the first number.
            Console.Write("Type a number, and then press:");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" Enter");
            Console.ResetColor();
            Console.Write("\nAlternatively, if you'd like to use a previous result - type: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("M");
            Console.WriteLine();
            Console.ResetColor();
            numInput1 = Console.ReadLine();
            numInput1 = UseHistoricResult(previousCalculationsList, numInput1);
            double cleanNum1 = 0;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput1 = Console.ReadLine();
            }

            // Ask the user to type the second number.
            Console.Write("Type another number, and then press: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Enter");
            Console.ResetColor();
            Console.Write("\nAlternatively, if you'd like to use a previous result - please type: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("M");
            Console.WriteLine();
            Console.ResetColor();
            numInput2 = Console.ReadLine();
            numInput2 = UseHistoricResult(previousCalculationsList, numInput2);

            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput2 = Console.ReadLine();
            }

            // Ask the user to choose an operator.
            void WriteOption(string command, string description)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"\t{command}");
                Console.ResetColor();
                Console.WriteLine($"- {description}");
            }
            WriteOption("a ", "Add");
            WriteOption("s ", "Subtract");
            WriteOption("m ", "Multiply");
            WriteOption("d ", "Divide");
            WriteOption("p ", "Power function");
            WriteOption("10x ", "10 to the power of");
            WriteOption("sq ", "Square root (only uses the first number)");
            WriteOption("sin ", "Sine angle (only uses the first number)");
            WriteOption("cos ", "Cosine angle (only uses the first number)");
            WriteOption("tan ", "Tangent angle (only uses the first number)");

            Console.WriteLine("\nYour option?\n ");
            string? op = Console.ReadLine();

            // Validate input is not null, and matches the pattern
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d|p|sq|10x|sin|cos|tan]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        calculationCount++;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(" Result # {0}:", calculationCount);
                        Console.ResetColor();
                        Console.WriteLine("\t{0:0.##}\n", result);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            WriteOption("n ", "Quit the app");
            WriteOption("Enter ", "Continue");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n");
        }
        calculator.Finish();
        return;
    }

    private static string? UseHistoricResult(List<(string, double)> previousCalculationsList, string? numInput1)
    {
        if (numInput1 != null && numInput1.ToUpper() == "M")
        {
            if (previousCalculationsList.Count == 0)
            {
                Console.WriteLine("No previous calculations found. Please enter a numeric value.");
                numInput1 = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Please select which previous result to use by typing the corresponding index:");
                for (int i = 0; i < previousCalculationsList.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($"{i + 1} ");
                    Console.ResetColor();
                    Console.Write($": {previousCalculationsList[i].Item1} {previousCalculationsList[i].Item2}");
                }
                string? playerInput = Console.ReadLine();
                int selectedIndex = -1;
                while (!int.TryParse(playerInput, out selectedIndex) || selectedIndex < 1 || selectedIndex > previousCalculationsList.Count)
                {
                    Console.Write("This is not valid input. Please enter a valid number corresponding to a previous result: ");
                    playerInput = Console.ReadLine();
                }
                numInput1 = previousCalculationsList[selectedIndex - 1].Item2.ToString();
            }
        }

        return numInput1;
    }
}