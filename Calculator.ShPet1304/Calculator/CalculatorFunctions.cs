using CalculatorLibrary;
using CalculatorMenu;
using Spectre.Console;


namespace CalculatorProgram
{
    public class CalculatorFunctions
    {
        public bool reusedResult = false;
        public double reusedNumber = 0;

        public void Function()
        {
            bool endApp = false;

            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            CalculatorLibrary.Calculator calculator = Calculator.Instance;

            while (!endApp)
            {
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;
                double cleanNum1 = 0;
                double cleanNum2 = 0;

                var menu = new Menu();
                string op = menu.ShowOperationsMenu();

                if (op == "Square Root" || op == "10x" || op == "Trigonometric function (sin)" || op == "Trigonometric function (cos)" || op == "Trigonometric function (tan)")
                {
                    if (cleanNum1 == 0 && !reusedResult)
                    {
                        Console.Write("Type a number, and then press Enter: ");
                        numInput1 = Console.ReadLine();

                        while (!double.TryParse(numInput1, out cleanNum1))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput1 = Console.ReadLine();
                        }
                        ;
                    }
                    else if (reusedResult)
                    {
                        cleanNum1 = reusedNumber;
                        reusedResult = false;
                        Console.WriteLine("Reusing the previous result: {0}\n", cleanNum1);
                    }
                    try
                    {
                        result = calculator.DoOperation(cleanNum1, 0, op);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else
                            Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                else if (cleanNum1 == 0 && !reusedResult)
                {
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();
                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput1 = Console.ReadLine();
                    }
                }

                else if (reusedResult)
                {
                    cleanNum1 = reusedNumber;
                    reusedResult = false;
                    Console.WriteLine("Reusing the previous result: {0}\n", cleanNum1);
                }
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput2 = Console.ReadLine();
                }
                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
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
                AnsiConsole.MarkupLine("\nPress [grey]Enter[/] to return to menu...");
                Console.ReadKey();
                break;
            }
        }
    }
}



