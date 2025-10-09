using System.Text.RegularExpressions;
using CalculatorLibrary;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");
        Calculator calculator = new Calculator();

        bool endApp = false;
        while (!endApp)
        {
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;

            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\th - History");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tr - Square Root");
            Console.WriteLine("\te - Exponentiation");
            Console.WriteLine("\ti - Sine");
            Console.WriteLine("\tc - Cosine");
            Console.WriteLine("\tt - Tangent");
            Console.WriteLine("\tx - 10x");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            if (op.ToLower() == "h")
            {
                calculator.CheckHistory();
            }
            else if (op == null || !Regex.IsMatch(op, "[a|s|m|d|r|e|i|c|t|x]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }
                double cleanNum2 = 0;
                if (Regex.IsMatch(op, "[a|s|m|d|e]")){
                    Console.Write("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();

                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput2 = Console.ReadLine();
                    }
                }
                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("------------------------\n");

            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.Clear();
        }
        calculator.Finish();
        return;
    }
}