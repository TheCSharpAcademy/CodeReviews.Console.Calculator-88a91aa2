using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram.m1chael888
{
    class Program
    {
        static void Main(string[] args)
        {
            var calc = new Calculator();
            bool done = false;

            Console.WriteLine("Console Calculator in C#");
            Console.WriteLine("------------------------\n");

            while (!done)
            {
                string? input1 = "";
                string? input2 = "";
                double result = 0;

                Console.Write("Type a number, and then press Enter: ");
                input1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(input1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    input1 = Console.ReadLine();
                }

                Console.Write("Type another number, and then press Enter: ");
                input2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(input2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    input2 = Console.ReadLine();
                }

                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.Write("Your choice: ");

                string? op = Console.ReadLine();

                if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
                {
                    Console.WriteLine("Error: Unrecognized input");
                }
                else
                {
                    try
                    {
                        result = calc.DoOperation(cleanNum1, cleanNum2, op);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a math error\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + ex.Message);
                    }
                }
                Console.WriteLine("------------------------\n");

                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") done = true;
                Console.WriteLine("\n");
            }
            calc.Finish();
            return;
        }
    }
}
