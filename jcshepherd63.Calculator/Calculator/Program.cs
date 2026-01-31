using System.Linq.Expressions;
using System.Text.RegularExpressions;
using CalculatorLibrary;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        int calculationCount = 0;
        List<string> previousCalculations = new();
        double ans = 0;
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("-------------------------\n");

        Calculator calculator = new();
        while (!endApp)
        {
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;

            if(calculationCount > 0)
            {
                Console.Write("Would you like to view past calculations? (Y/N): ");
                string? pastCalcRead = Console.ReadLine().ToLower().Trim();
                if(pastCalcRead == "y")
                {
                    foreach(string calc in previousCalculations)
                    {
                        Console.WriteLine(calc);
                    }
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                }
            }

            Console.Write("Type a number or 'Ans' (to use the result from the last calculation), and then press enter: ");
            numInput1 = Console.ReadLine();

            double cleanNum1 = 0;
            if (numInput1.ToLower().Trim() == "ans")
            {
                cleanNum1 = ans;
            }
            else
            {
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.WriteLine("This is not a valid input. Please enter a number.");
                    numInput1 = Console.ReadLine();
                }
            }

            Console.Write("Type a number or 'Ans' (to use the result from the last calculation), and then press enter: ");
            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;

            if (numInput2.ToLower().Trim() == "ans")
            {
                cleanNum2 = ans;
            }
            else
            {
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.WriteLine("This is not a valid input. Please enter a number or 'Ans'.");
                    numInput2 = Console.ReadLine();
                }
            }

            Console.WriteLine("Choose an operator from the list below: \n -------------------------------------------");
            Console.WriteLine("\tA - Add");
            Console.WriteLine("\tS - Subtract");
            Console.WriteLine("\tM - Multiply");
            Console.WriteLine("\tD - Divide");
            Console.Write("Choose one and press enter: ");

            string? op = Console.ReadLine().ToLower().Trim();
            if (op == null || ! Regex.IsMatch(op, "[a|s|m|d]"))
            {
                Console.WriteLine("Error: Unrecognized Input.");
            }
            else
            {
                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    ans = result;
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This will result in a mathematical error. \n");
                    }
                    else
                    {
                        Console.WriteLine($"Your result: {result}");
                        calculationCount++;
                        switch (op)
                        {
                            case "a":
                                previousCalculations.Add($"{cleanNum1} + {cleanNum2} = {result}");
                                break;
                            case "s":
                                previousCalculations.Add($"{cleanNum1} - {cleanNum2} = {result}");
                                break;
                            case "m":
                                previousCalculations.Add($"{cleanNum1} * {cleanNum2} = {result}");
                                break;
                            case "d":
                                previousCalculations.Add($"{cleanNum1} / {cleanNum2} = {result}");
                                break;
                        }
                        Console.WriteLine($"You have used the calculator {calculationCount} times in this session.");
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("An error occured while trying to process this operation.");
                }
            }
            Console.WriteLine("------------------------------------------------\n");

            Console.WriteLine("Type 'End' then Enter to close the app or press any other key and Enter to continue.");
            if(Console.ReadLine().ToLower().Trim() == "end")
            {
                endApp = true;
            }
                Console.WriteLine("\n");
        }
        calculator.Finish();
        return;
    }
}

//Merged with branch to complete part 1.