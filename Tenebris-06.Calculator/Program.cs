using System.Text.RegularExpressions;
using CalculatorLibrary;
using System.Diagnostics;


class Program
{
    static void Main(string[] args)
    {
        var data = History.data;
        bool endApp = false;
        bool mainMenu = true;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();

        while (!endApp)
        {
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            string? numInput1;
            string? numInput2;
            double  result = 0;
            string? choice;

            while (mainMenu)
            {
                // Ask the user to specify choice
            Console.WriteLine("Choose an Option:");

            Console.WriteLine("[1] Calculate");
            Console.WriteLine("[2] Show History");
            Console.WriteLine("[3] Delete History");
            
            choice = Console.ReadLine();

            if (choice == null || !Regex.IsMatch(choice, "^[123]$"))
            {
                Console.WriteLine("Please input a valid choice:");
            } else if (choice == "1")
            {
                Console.WriteLine(new string('-', Console.WindowWidth));
                mainMenu = false;
            } else if (choice == "2")
            {
                History.ShowHistory();

            } else if (choice == "3")
            {
                History.DeleteHistory();
            }
            
            } 

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("a - Add");
            Console.WriteLine("s - Subtract");
            Console.WriteLine("m - Multiply");
            Console.WriteLine("d - Divide\n");

            Console.WriteLine("q - Square Root");
            Console.WriteLine("p - Power");
            Console.WriteLine("10x - 10x\n");

            Console.WriteLine("sin - Sin(x)");
            Console.WriteLine("cos - Cos(x)");
            Console.WriteLine("tan - Tan(x)\n");

            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            Console.WriteLine("\n");


            

            
            // Validate input is not null, and matches the pattern
            if (op == null || ! Regex.IsMatch(op, "^(a|s|m|d|q|p|10x|sin|cos|tan)$", RegexOptions.IgnoreCase))
            {
               Console.WriteLine("Error: Unrecognized input.");
            }
            else
            { 
                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }
                
               try
                {

                    if (Regex.IsMatch(op, "^(a|s|m|d)$", RegexOptions.IgnoreCase))
                    {
                        // Ask the user to type the second number.
                        Console.Write("Type another number, and then press Enter: ");
                        numInput2 = Console.ReadLine();

                        double cleanNum2 = 0;
                        while (!double.TryParse(numInput2, out cleanNum2))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput2 = Console.ReadLine();
                        }

                        result = calculator.DoOperation(cleanNum1, cleanNum2, op);

                    } else
                    {
                        result = calculator.DoOperation(cleanNum1, op);
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
                Console.WriteLine(new string('-', Console.WindowWidth));

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;
                
                // Go Back to Main Menu
                mainMenu = true;

                Console.WriteLine("\n"); // Friendly linespacing.
        }
        return;
    }
}