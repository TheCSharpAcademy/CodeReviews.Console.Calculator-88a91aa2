using CalculatorLibrary;
using static CalculatorLibrary.Calculator;


class Program
{ 
    static void Main(string[] args)
    {
        bool endApp = false;
        Calculator calculator = new Calculator();

        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");


        while (!endApp)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("\t1 - New calculation");
            Console.WriteLine("\t2 - View history");
            Console.WriteLine("\t3 - Use previous result");
            Console.WriteLine("\t4 - Clear history");
            Console.WriteLine("\tx - Exit");
            Console.Write("Your choice: ");

            string mainChoice = Console.ReadLine();

            if (mainChoice == "x")
            {
                endApp = true;
                continue;
            }

            if (mainChoice == "2")
            {
                PrintHistory(calculator);
                continue;
            }

            if (mainChoice == "4")
            {
                calculator.ClearHistory();
                Console.WriteLine("History cleared.\n");
                continue;
            }

            double? reusedValue = null;

            if (mainChoice == "3")
            {
                reusedValue = SelectHistoryResult(calculator);
                if (reusedValue == null)
                    continue; // user cancelled or invalid
            }


            double? num1 = null;
            double? num2 = null;
            double result = 0;

            PrintOperationMenu(calculator.OperationLabels);

            string? opInput = Console.ReadLine();

            OperationType operation;

            try
            {
                operation = calculator.MapInputToOperation(opInput ?? "");
            }
            catch
            {
                Console.WriteLine("Error: Unrecognized input.");
                continue;
            }

            int count = calculator.GetOperandCount(operation);

            if (count >= 1)
            {
                if (reusedValue != null)
                {
                    num1 = reusedValue;
                    Console.WriteLine($"Using previous result: {num1}");
                }
                else
                {
                    Console.Write("Enter first number: ");
                    num1 = ReadDouble();
                }
            }


            if (count == 2)
            {
                Console.Write("Enter second number: ");
                num2 = ReadDouble();
            }

            try
            {
                result = calculator.DoOperation(operation, num1 ?? 0, num2);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else Console.WriteLine($"Your result for game number {calculator.UsageCount}: {result:0.##}\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            Console.WriteLine("------------------------\n");

            Console.Write("Press 'x' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "x") endApp = true;

            Console.WriteLine("\n"); 
        }

        calculator.Finish();
        return;
    }

    private static double? ReadDouble()
    {
        string? inputNum = Console.ReadLine();
        double cleanNum = 0;
        while (!double.TryParse(inputNum, out cleanNum))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            inputNum = Console.ReadLine();
        }
        return cleanNum; 
    }

    private static void PrintOperationMenu(
    IReadOnlyDictionary<OperationType, string> labels)
    {
        Console.WriteLine("Choose an operator from the following list:");

        foreach (var entry in labels)
            Console.WriteLine($"\t{entry.Value}");

        Console.Write("Your option: ");
    }

    private static void PrintHistory(Calculator calculator)
    {
        var history = calculator.GetHistory();

        if (history.Count == 0)
        {
            Console.WriteLine("History is empty.\n");
            return;
        }

        Console.WriteLine("Calculation History:");
        for (int i = 0; i < history.Count; i++)
        {
            var h = history[i];
            Console.WriteLine($"{i}: {h.Operand1} {h.Operation} {h.Operand2} = {h.Result}");
        }
        Console.WriteLine();
    }

    private static double? SelectHistoryResult(Calculator calculator)
    {
        var history = calculator.GetHistory();

        if (history.Count == 0)
        {
            Console.WriteLine("History is empty.\n");
            return null;
        }

        PrintHistory(calculator);

        Console.Write("Enter the index of the result to reuse: ");
        if (int.TryParse(Console.ReadLine(), out int index))
        {
            if (index >= 0 && index < history.Count)
                return history[index].Result;
        }

        Console.WriteLine("Invalid selection.\n");
        return null;
    }



}