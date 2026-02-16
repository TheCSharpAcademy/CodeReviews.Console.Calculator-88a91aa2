using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorApp
{
    class Program
    {
        public static void  Main(string[] args)
        {
            string? userInput;
            Calculator calculator = new Calculator();
            do
            {
                bool endApp = false;
                int calculatorUsed = 0;
                Console.WriteLine("======Calculator Console App======");
                Console.WriteLine("What you would like to do?\n 1 - Calculation operations\n 2 - Calculation records\n " +
                    "3 - Delete calculation records\n 4 - Exit ");
                userInput = Console.ReadLine();
                //to check the user input, it must be numeric 
                while(userInput == null || !Regex.IsMatch(userInput , "[1 - 4]"))
                {
                    Console.WriteLine("Please enter choose from 1 to 4. ");
                    Console.ReadLine();
                }

                switch(userInput)
                {
                    case "1":
                        bool userPreviousResult = false;
                        while(!endApp)
                        {
                            //Declare variables and set to empty
                            //Use Nullable types(with ?) to match type os System.Console.Readline
                            string? firstNumber = "";
                            string? secondNumber = "";
                            double result = 0;
                            Console.Clear();

                            //Ask the firstnumber to be enter
                            double cleanNumber1 = 0;
                            if(!userPreviousResult)
                            {
                                Console.WriteLine("Please type a number and press enter: ");
                                firstNumber = Console.ReadLine();
                                while(!double.TryParse(firstNumber, out cleanNumber1))
                                {
                                    Console.WriteLine("This is not a number value. Please type a number value.");
                                    firstNumber = Console.ReadLine();
                                }
                            }
                            else
                            {
                                cleanNumber1 = calculator.ResultList.Last().Answer;
                                Console.WriteLine($"Previous Result: {cleanNumber1}");
                            }

                            //Choose an operator
                            Console.WriteLine("\t+ - Add\n\t- - Subtract\n\t* - Multiply\n\t/ - Divide\n\t^ - Power\n\tr - Square root\n\tS - Sin\n\tC - Cosine\n\tT - Tangent");
                            Console.WriteLine("Your option?");
                            string? opera = Console.ReadLine();
                            //Validate the input is  not null, and match the patterns
                            while(opera == null || !Regex.IsMatch(opera , @"[\+|\-|\*|/|^|r|s|c|t]"))
                            {
                                Console.WriteLine("Error: Unrecognized value");
                            }
                            opera.Trim().Substring(0 , 1);
                            //Ask the second number to be enter
                            double cleanNumber2 = 0;
                            
                            if(Regex.IsMatch(opera , @"[\+|\-|\*|/|^]"))
                            {
                                Console.WriteLine("Type the another number and press enter:");
                                secondNumber = Console.ReadLine();
                                while(!double.TryParse(secondNumber, out cleanNumber2))
                                {
                                    Console.WriteLine("This is not a number, Please type a number value");
                                    secondNumber = Console.ReadLine();
                                }
                            }

                            try
                            {
                                result = calculator.DoCalculation(cleanNumber1, cleanNumber2, opera);
                                calculatorUsed++;
                                if (double.IsNaN(result))
                                {
                                    Console.WriteLine("This operation will result an arithmatical error.");
                                }
                                else Console.WriteLine("Your result: {0:0.##}\n", result);

                            }
                            catch(Exception e)
                            {
                                Console.WriteLine("Oh no! An exception occured trying to do math operation.\n - Details: " + e.Message);
                            }

                            Console.WriteLine("------------------------\n");
                            Console.WriteLine($"Calculator was used {calculatorUsed} times");
                            Console.WriteLine("Press 'x' to discard previous result and start new operaion: ");
                            Console.Write("Press 'n' and Enter to go to menu, or press any other key and Enter to resume working on previous result: ");

                            string? endOption = Console.ReadLine();
                            if (endOption == "n" )
                            {
                                endApp = true;
                            }
                            else if(endOption != "x")
                            {
                                userPreviousResult = true;
                            }
                            else
                            {
                                userPreviousResult = false;
                            }
                            Console.WriteLine("\n");
                        }
                        calculator.Finish();
                        break;
                    case "2":
                        Console.Clear();
                        if(calculator.ResultList.Count == 0)
                        {
                            Console.WriteLine("===Currently there are no calculation history===");
                        }
                        else
                        {
                            Console.WriteLine("===ALL THE CALCULATION HISTORY===\n");
                            foreach(var result in calculator.ResultList)
                            {
                                result.Display();
                            }
                        }
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Are you sure you want to delete all the calculation history? (Y/N)");
                        string? answer = Console.ReadLine();
                        while(answer == null || (answer != "y" && answer != "n"))
                        {
                            Console.WriteLine("Please choose the correct option. (y/n)");
                            answer = Console.ReadLine();
                        }
                        if(answer == "y")
                        {
                            calculator.ResultList.Clear();
                            Console.Clear();
                            Console.WriteLine("All the calculation histroy are deleted successfully. Press enter.");
                            Console.ReadLine();
                        }
                        break;
                }
            } while (userInput != "4");
        }
    }
}