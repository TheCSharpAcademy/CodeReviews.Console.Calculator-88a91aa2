using CalculatorLibrary;
using System.Text.RegularExpressions;

int calculatorUsedNumber = 0;
bool endApp = false;
List<string> calculations = new List<string>();

Console.WriteLine("Console Calculator in C#\r");
Console.WriteLine("------------------------\n");

Calculator calculator = new Calculator();
while (!endApp)
{
    string? numInput1 = "";
    string? numInput2 = "";
    double result = 0;

    Console.WriteLine("Enter 1 to show history list");
    Console.WriteLine("Press enter to do calculation");

    if (Console.ReadLine() == "1")
    {
        Console.WriteLine();

        if (calculations.Count == 0)
        {
            Console.WriteLine("No history list");
        }
        else
        {
            for (int i = 0; i < calculations.Count; i++)
            {
                Console.WriteLine(calculations[i]);
            }
        }
        Console.WriteLine();
        continue;
    }

    Console.Write("Type a number, and then press Enter: ");
    numInput1 = Console.ReadLine();

    double cleanNum1 = 0;
    while (!double.TryParse(numInput1, out cleanNum1))
    {
        Console.Write("This is not valid input. Please enter an integer value: ");
        numInput1 = Console.ReadLine();
    }

    Console.WriteLine("Choose an operator from the following list:");
    Console.WriteLine("\ta - Add");
    Console.WriteLine("\ts - Subtract");
    Console.WriteLine("\tm - Multiply");
    Console.WriteLine("\td - Divide");
    Console.WriteLine("\tr - Square root");
    Console.WriteLine("\tp - Power");
    Console.WriteLine("\tsin - sine");
    Console.WriteLine("\tcos - cosine");
    Console.WriteLine("\ttan - Tangent");
    Console.Write("Your option? ");

    string? op = Console.ReadLine();

    if (op == null || !Regex.IsMatch(op, "[a|s|m|d|r|p|sin|cos|tan]"))
    {
        Console.WriteLine("Error: Unrecognized input.");
    }
    else
    {
        try
        {
            if (!Regex.IsMatch(op, "^(r|sin|cos|tan)$"))
            {
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }

                result = calculator.DoOperation(cleanNum1, op, cleanNum2);
                AddCalculationToList(cleanNum1, op, result, cleanNum2);
            }
            else
            {
                result = calculator.DoOperation(cleanNum1, op);
                AddCalculationToList(cleanNum1, op, result);
            }

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

    Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
    if (Console.ReadLine() == "n") endApp = true;

    Console.WriteLine("\n");

    calculatorUsedNumber++;
}

Console.WriteLine($"Calculator used {calculatorUsedNumber} times.");
calculator.Finish();
return;


void AddCalculationToList(double num1, string op, double result, double num2 = 0)
{
    switch (op)
    {
        case "a":
            calculations.Add($"{num1} + {num2} = {result}");
            break;
        case "s":
            calculations.Add($"{num1} - {num2} = {result}");
            break;
        case "m":
            calculations.Add($"{num1} * {num2} = {result}");
            break;
        case "d":
            calculations.Add($"{num1} / {num2} = {result}");
            break;
        case "r":
            calculations.Add($"√{num1} = {result}");
            break;
        case "p":
            calculations.Add($"{num1}^{num2} = {result}");
            break;
        case "sin":
            calculations.Add($"sin({num1}) = {result}");
            break;
        case "cos":
            calculations.Add($"cos({num1}) = {result}");
            break;
        case "tan":
            calculations.Add($"tan({num1}) = {result}");
            break;
    }
}