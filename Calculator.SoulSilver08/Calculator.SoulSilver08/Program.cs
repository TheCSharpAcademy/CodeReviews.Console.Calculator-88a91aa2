using System.Text.RegularExpressions;
using CalculatorLibrary;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        Calculator calculator = new();
        List<string> lastCalculations = [];

        while (!endApp)
        {
            Console.Clear();
            Console.Write("Console Calculator in C#");
            if (lastCalculations.Count > 0)
                Console.WriteLine($"\t{lastCalculations.Count} Times used");
            else Console.WriteLine();
            Console.WriteLine("------------------------\n");

            string? numInput1 = null;
            string? numInput2 = null;
            double result;

            //First number input
            while (numInput1 == null)
            {
                Console.Write("Type the first number, and then press Enter");
                if (lastCalculations.Count > 0)
                    Console.Write(", Or type h and then press Enter to see the last calculation history");
                Console.Write(": ");
                numInput1 = Console.ReadLine();
                if (numInput1 == "h" && lastCalculations.Count > 0)
                    numInput1 = LastCalculations(lastCalculations);
            }

            double cleanNum1;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("Ivalid input. Type another number, and then press Enter: ");
                numInput1 = Console.ReadLine();
            }

            //Operator Input
            Console.WriteLine("\nChoose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tr - SquareRoot");
            Console.WriteLine("\tp - Power");
            Console.WriteLine("\te - Exponencial");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            //Second number input
            double cleanNum2 = 0;
            if (op != "r" && op != "e")
            {
                while (numInput2 == null)
                {
                    Console.Write("\nType the second number, and then press Enter");
                    if (lastCalculations.Count > 0)
                        Console.Write(", Or type h and then press Enter to see the last calculation history");
                    Console.Write(": ");
                    numInput2 = Console.ReadLine();
                    if (numInput2 == "h" && lastCalculations.Count > 0)
                        numInput2 = LastCalculations(lastCalculations);
                }

                //double cleanNum2;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("Invalid input. Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();
                }
            }

            if (op == null || !Regex.IsMatch(op, "[a|s|m|d|r|p|e]"))
                Console.WriteLine("Error: Unrecognized input.");
            else
            {
                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                        if (op == "r" || op == "e")
                            lastCalculations.Add($"{cleanNum1} {calculator.LetterToSign(op)} = {result}");
                        else
                            lastCalculations.Add($"{cleanNum1} {calculator.LetterToSign(op)} {cleanNum2} = {result}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("------------------------\n");

            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n")
                endApp = true;

            Console.WriteLine("\n");
        }
        calculator.Finish();

        return;
    }

    public static string? LastCalculations(List<string> lastCalculations) 
    {
        int count = 0;
        string? resultSelection;

        Console.WriteLine("Last Calculations:");
        Console.WriteLine("------------------------\n");
        foreach (string calculation in lastCalculations) 
        {
            count++;
            Console.WriteLine($"[{count}]: {calculation}");
        }
        Console.WriteLine("What do you want to do?:");
        Console.WriteLine("\tu - Use a result as an input");
        Console.WriteLine("\td - Delete last calculation history");
        Console.WriteLine("\tn - nothing");
        Console.Write("Your option? ");
        string? optionSelection = Console.ReadLine();

        while (optionSelection == null || !Regex.IsMatch(optionSelection, "[u|d|n]")) 
        {
            Console.Write("Ivalid input. Type another option, and then press Enter: ");
            optionSelection = Console.ReadLine();
        }

        switch (optionSelection) 
        {
            case "u":
                Console.WriteLine("type the option number of the desire result");
                resultSelection = Console.ReadLine();
                int option;
                while (!int.TryParse(resultSelection, out option) || option > lastCalculations.Count || option < 1) 
                {
                    Console.Write("Invalid input. Please enter a valid input:");
                    resultSelection = Console.ReadLine();
                }
                return lastCalculations[option - 1].Remove(0, lastCalculations[option - 1].IndexOf('=') + 1);
            case "d":
                lastCalculations.Clear();
                Console.WriteLine("The Last Calculations History has been deleted");
                return null;
            case "n":
                return null;
        }
        Console.WriteLine("\n------------------------\n");

        return null;
    }
}