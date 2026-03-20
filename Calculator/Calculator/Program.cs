using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram
{

    public class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            int calcCtr = 0;
            List<Calculations> calculatorHistory = new List<Calculations>();
            Calculator calculator = new Calculator();
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            while (!endApp)
            {
                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                Console.Clear();
                string? userInput1 = "";
                string? userInput2 = "";
                double result = 0;

                // Ask the user to type the first number or list to see history.
                Console.Write("Type a number or \"list\" to see your history, and then press Enter: ");
                userInput1 = Console.ReadLine();
                string notNullUserInput1 = userInput1 ?? "";
                double cleanNum1 = 0;
                if (notNullUserInput1.ToLower().Equals("list"))
                {
                    if (calculatorHistory.Count == 0)
                    {
                        Console.WriteLine("No calculations yet! Press enter to continue..");
                        Console.ReadLine();
                        continue;
                    }
                    int i = 1;
                    foreach (Calculations calc in calculatorHistory)
                    {
                        Console.WriteLine("(" + i + ") " + calc.ToString());
                        i++;
                    }
                    Console.WriteLine("Enter the index of the calculation result you want to reuse, Enter clear to delete history" +
                        "\nor press Enter to continue: ");
                    string? indexInput = Console.ReadLine();
                    string notNullIndexInput = indexInput ?? "";
                    if (notNullIndexInput.Equals(""))
                    {
                        Console.WriteLine("Continuing without selecting a previous result.");
                        continue;
                    }
                    else if (notNullIndexInput.ToLower().Equals("clear"))
                    {
                        calculatorHistory.Clear();
                        Console.WriteLine("History cleared! Press enter to continue..");
                        Console.ReadLine();
                        continue;
                    }

                    if (!int.TryParse(notNullIndexInput, out int indexInputInt))
                    {
                        Console.Write("This is not valid input.");
                    }
                    else if (indexInputInt > 0 && indexInputInt <= calculatorHistory.Count)
                    {
                        cleanNum1 = calculatorHistory[indexInputInt - 1].result;
                        Console.WriteLine("You have selected: " + cleanNum1);
                    }
                }
                else
                {
                    while (!double.TryParse(userInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value or \"list\": ");
                        userInput1 = Console.ReadLine();
                    }
                }



                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                userInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(userInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    userInput2 = Console.ReadLine();
                }

                string? op = "";

                while (true)
                {
                    // Ask the user to choose an operator.
                    Console.WriteLine("First Number: " + cleanNum1);
                    Console.WriteLine("Second Number: " + cleanNum2);
                    Console.WriteLine("Choose an operator from the following list:");
                    Console.WriteLine("\ta - Add");
                    Console.WriteLine("\ts - Subtract");
                    Console.WriteLine("\tm - Multiply");
                    Console.WriteLine("\td - Divide");
                    Console.WriteLine("\tp - power");
                    Console.WriteLine("\tmod - modulus (remainder)");
                    Console.Write("Your option? ");

                    op = Console.ReadLine();
                    op = op?.ToLower();

                    // Validate input is not null, and matches the pattern
                    if (op == null || !Regex.IsMatch(op, "^(a|s|m|d|p|mod)$"))
                    {
                        Console.WriteLine("Error: Unrecognized input. Press enter to retry");
                        Console.ReadLine();
                        Console.Clear();
                    }
                    else
                    {
                        break;
                    }
                }

                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                    calcCtr++;
                    calculatorHistory.Add(new Calculations
                    {
                        number1 = (int)cleanNum1,
                        number2 = (int)cleanNum2,
                        operation = op,
                        result =result
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.WriteLine("Calculations so far: " + calcCtr);
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }

        public struct Calculations
        {
            public int number1;
            public int number2;
            public string operation;
            public double result;

            public Calculations(int num1, int num2, string op, double res)
            {
                number1 = num1;
                number2 = num2;
                operation = op;
                result = res;
            }

            private String ConvertOperationToSymbol(string operation)
            {
                switch (operation)
                {
                    case "a":
                        return "+";
                    case "s":
                        return "-";
                    case "m":
                        return "*";
                    case "d":
                        return "/";
                    case "p":
                        return "^";
                    case "mod":
                        return "%";
                    default:
                        return operation;
                }
            }
            public override string ToString()
            {
                return $"{number1} {ConvertOperationToSymbol(operation)} {number2} = {result}";
            }
        }
    }
}