using System.Text.RegularExpressions;
using CalculatorLibrary;

bool endApp = false;

Console.WriteLine("Console Calculator in C#\r");
Console.WriteLine("------------------------\n");

Calculator calculator = new Calculator();
while (!endApp)
{
    string? numInput1;
    string? numInput2;
    double result;

    Console.Write("Type a number, and then press Enter: ");
    numInput1 = Console.ReadLine();

    double cleanNum1;
    while (!double.TryParse(numInput1, out cleanNum1))
    {
        Console.Write("This is not valid input. Please enter an integer value: ");
        numInput1 = Console.ReadLine();
    }

    Console.Write("Type another number, and then press Enter: ");
    numInput2 = Console.ReadLine();

    double cleanNum2;
    while (!double.TryParse(numInput2, out cleanNum2))
    {
        Console.Write("This is not valid input. Please enter an integer value: ");
        numInput2 = Console.ReadLine();
    }

    Console.WriteLine("Choose an operator from the following list:");
    Console.WriteLine("\ta - Add");
    Console.WriteLine("\ts - Subtract");
    Console.WriteLine("\tm - Multiply");
    Console.WriteLine("\td - Divide");
    Console.Write("Your option? ");

    string? op = Console.ReadLine();

    if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
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
                Console.WriteLine("Your result: {0:0.##}\n", result);
        }
        catch (Exception e)
        {
            Console.WriteLine(
                "Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message
            );
        }
    }

    var history = calculator.GetHistory();
    if (history.Count > 0)
    {
        Console.WriteLine("Calculation History:");
        foreach (var entry in history)
        {
            Console.WriteLine(entry);
        }
    }
    else
    {
        Console.WriteLine("Calculation History is empty.");
    }

    Console.WriteLine("------------------------\n");

    Console.Write(
        "Type 'clear' to delete calculation history, 'n' and Enter to close the app, \nor press any other key and Enter to continue: "
    );
    var userInput = Console.ReadLine();
    if (userInput == "n")
    {
        endApp = true;
    }
    else if (userInput == "clear")
    {
        calculator.ClearHistory();
        Console.WriteLine("Calculation history cleared.\n");
    }

    Console.WriteLine("\n");
}
calculator.Finish();
Console.WriteLine($"Calculator was used {calculator.UsageCount} times.");
return;
