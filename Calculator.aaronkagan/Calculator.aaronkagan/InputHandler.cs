using System.Text.RegularExpressions;

namespace CalculatorProgram;

class InputHandler
{
    public double GetValidNumber(CalculationHistory history)
    {
        if (history.Count() > 0)
        {
            Console.WriteLine("Would you like to get the number from results history?  y for yes or any other key for no: ");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.WriteLine();
                double historyResult = GetValidHistoryResult(history);
                return historyResult;
            }
        }
        
        Console.WriteLine();
        Console.WriteLine("Please enter a number: ");
        string? numInput = Console.ReadLine();
        double cleanNum;
        while (!double.TryParse(numInput, out cleanNum))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            numInput = Console.ReadLine();
        }
        return cleanNum;
    }
    private double GetValidHistoryResult(CalculationHistory history)
    {
        history.Print();
        Console.WriteLine("Which result from history would you like to use? Enter the line number then press enter: ");
        string? input = Console.ReadLine();
        int cleanNum;
            
        while (!int.TryParse(input, out cleanNum) ||  cleanNum > history.Count() || cleanNum < 0)
        {
            Console.Write("This is not valid input. Please enter a line number that exists: ");
            input = Console.ReadLine();

            int.TryParse(input, out cleanNum);
        }

        var index = cleanNum - 1;

        return history.GetResult(index);
    }

    public string GetValidOperation()
    {
        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.Write("Your option? ");
        string? op;
        do
        {
            op = Console.ReadLine();
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
            {
                Console.WriteLine("Invalid Operation. Please try again.");
            }
        } while (op == null || !Regex.IsMatch(op, "[a|s|m|d]"));

        return op;
    }
}