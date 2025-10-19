using Spectre.Console;
using System;

namespace Calculator;
internal class Operations
{
    public double Result { get; set; } = 0;
    public string OperationName { get; set; } = "";
    public static int NumberOperationMade { get; set; } = 0;

    public static double ValidateInput(bool isDivision)
    {
        double result;

        if (isDivision)
        {
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out result))
                {
                    if (result <= 0)
                    {
                        AnsiConsole.MarkupLine("[#FF5757]Please enter a number greater than 0[/]");
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("[#FF5757]Please enter a valid number[/]");
                }
            }
        }
        else
        {
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out result))
                {
                    break;
                }
                AnsiConsole.MarkupLine("[#FF5757]Please enter a valid number[/]");
            }
        }

        return result;
    }

    public bool UseLastOrSelectedResult()
    {
        string confirm;

        while (true)
        {
            Console.WriteLine("Do you want to use the last or result selected");
            Console.WriteLine("Press Y to confirm");
            confirm = Console.ReadLine();

            if (confirm != null)
            {
                break;
            }
        }


        if (confirm.ToLower() == "y")
        {
            return true;
        }

        return false;
    }
}

