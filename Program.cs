using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

class Calculator
{
    public static List<string> history = [];
    static int timesUsed = 0;

    public static double DoOperation(string op, double num1 = 0, double num2 = 0)
    {
        double result = double.NaN; // Default value is "not-a-number" which we use if an operation, such as division, could result in an error.
        bool wasUsed = true;

        string operationCharacter = "";
        string resultString = "";
        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                operationCharacter = "+";
                result = num1 + num2;
                resultString = $"{num1} {operationCharacter} {num2} = {result}";
                AddToHistory(resultString);
                break;
            case "s":
                operationCharacter = "-";
                result = num1 - num2;
                resultString = $"{num1} {operationCharacter} {num2} = {result}";
                AddToHistory(resultString);
                break;
            case "m":
                operationCharacter = "*";
                result = num1 * num2;
                resultString = $"{num1} {operationCharacter} {num2} = {result}";
                AddToHistory(resultString);
                break;
            case "d":
                operationCharacter = "/";
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                    resultString = $"{num1} {operationCharacter} {num2} = {result}";
                    AddToHistory(resultString);
                }
                break;
            case "sq":
                operationCharacter = "√";
                result = Math.Sqrt(num1);
                resultString = $"{operationCharacter}({num1}) = {result}";
                AddToHistory(resultString);
                break;
            case "p":
                operationCharacter = "^";
                result = Math.Pow(num1, num2);
                resultString = $"{num1} {operationCharacter} {num2} = {result}";
                AddToHistory(resultString);
                break;
            case "ten":
                operationCharacter = "* 10";
                result = num1 * 10;
                resultString = $"{num1} {operationCharacter} = {result}";
                AddToHistory(resultString);
                break;
            case "sin":
                operationCharacter = "sin";
                result = Math.Sin(num1);
                resultString = $"{operationCharacter}({num1}) = {result}";
                AddToHistory(resultString);
                break;
            case "cos":
                operationCharacter = "cos";
                result = Math.Cos(num1);
                resultString = $"{operationCharacter}({num1}) = {result}";
                AddToHistory(resultString);
                break;
            case "tan":
                operationCharacter = "tan";
                result = Math.Tan(num1);
                resultString = $"{operationCharacter}({num1}) = {result}";
                AddToHistory(resultString);
                break;
            default:
                wasUsed = false;
                break;
        }
        if (wasUsed && !Double.IsNaN(result))
        {
            timesUsed++;
        }
        return result;
    }

    public static void DoCommand(string command)
    {
        switch (command)
        {
            case "h":
                ShowHistory(history);
                Console.WriteLine("");
                break;
            case "del":
                DeleteHistory(history);
                break;
            case "u":
                try
                {
                    double lastResult = GetLastResult(history);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                break;
            case "c":
                int used = GetTimesUsed(timesUsed);
                Console.WriteLine($"Calculator was used {used} times");
                break;
        }
    }

    static void AddToHistory(string result)
    {
        history.Add($"{history.Count + 1}. {result}");
    }
    static void ShowHistory(List<string> history)
    {
        if (history.Count == 0)
        {
            Console.WriteLine("There are no recorded calculations.");
        }
        else
            foreach (string s in history)
            {
                Console.WriteLine(s);
            }
    }
    static void DeleteHistory(List<string> history)
    {
        if (history.Count == 0)
        {
            Console.WriteLine("There are no results to delete.");
        }
        else
        {
            Console.WriteLine("Deleting history...");
            history.Clear();
        }
    }

    static int GetTimesUsed(int timesUsed)
    {
        return timesUsed;
    }

    public static double GetLastResult(List<string> history)
    {
        if (history.Count == 0) { throw new InvalidOperationException("There are no results in history."); }

        string lastResult = history[history.Count - 1];
        string result = lastResult.Substring(lastResult.IndexOf('=') + 1);
        result = result.Trim();
        return Double.Parse(result);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        List<string> validOperators = ["a", "s", "m", "d", "sq", "p", "sin", "cos", "tan", "ten"];
        List<string> singleOperandOperators = ["sq", "sin", "cos", "tan", "ten"];
        List<string> calculatorCommands = ["c", "del", "h", "u"];

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
            Console.Write("Your option? ");

            string? op = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(op))
            {
                Console.WriteLine("Error: Unrecognized input.");
                Console.Write("Your option? ");
                op = Console.ReadLine();
            }

            op = op.Trim().ToLower();

            while (!validOperators.Contains(op) && !calculatorCommands.Contains(op))
            {
                Console.WriteLine("Error: Unrecognized input.");
                Console.Write("Your option? ");
                op = Console.ReadLine();
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
                    double result = Calculator.DoOperation(op, cleanNum1, cleanNum2);
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
                        double lastResult = Calculator.GetLastResult(Calculator.history);
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

                        double result = Calculator.DoOperation(newOp, lastResult, cleanNum2);
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
                    Calculator.DoCommand(op);
                }
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }
        return;
    }
}