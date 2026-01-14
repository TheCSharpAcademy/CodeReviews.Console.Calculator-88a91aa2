using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System;
using CalculatorLibrary;

namespace CalculatorApp
{

    class Program
    {
        static void Main(string[] args)
        {
            string? userInput;
            Calculator calculator = new Calculator();
            do
            {
                bool endApp = false;
                int calculatorUsed = 0;
                Console.Clear();
                Console.WriteLine("=====\tCalculator Menu Options\t=====");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("Press '1' for calculation operations.");
                Console.WriteLine("Press '2' for viewing and using calculations records.");
                Console.WriteLine("Press '3' for deleting calculation records.");
                Console.WriteLine("Press '4' for existing the program.");

                userInput = Console.ReadLine();
                //Checking the userinput value to be numeric values
                while (userInput == null || !Regex.IsMatch(userInput, "[1-4]"))
                {
                    Console.WriteLine("Please enter the value from 1 to 4");
                    userInput = Console.ReadLine();
                }

                //using switch statement
                switch (userInput)
                {
                    case "1":
                        bool usePreviousResult = false;
                        while (!endApp)
                        {
                            // Declare variables and set to empty.
                            // Use Nullable types (with ?) to match type of System.Console.ReadLine
                            string? numInput1 = "";
                            string? numInput2 = "";
                            double result = 0;
                            Console.Clear();

                            // Ask the user to type the first number.
                            double cleanNum1 = 0;
                            if (!usePreviousResult)
                            {
                                Console.Write("Type a number, and then press Enter: ");
                                numInput1 = Console.ReadLine();
                                while (!double.TryParse(numInput1, out cleanNum1))
                                {
                                    Console.Write("This is not valid input. Please enter an integer value: ");
                                    numInput1 = Console.ReadLine();
                                }
                            }
                            else
                            {
                                cleanNum1 = calculator.ResultList.Last().Answer;
                                Console.WriteLine($"Previous Result: {cleanNum1}");
                            }

                            // Ask the user to choose an operator.
                            Console.WriteLine("\t+ - Add\n\t- - Subtract\n\t* - Multiply\n\t/ - Divide\n\t^ - Power\n\tr - Square root\n\tS - Sin\n\tC - Cosine\n\tT - Tangent");
                            Console.Write("Your option? ");

                            string? op = Console.ReadLine();
                            // Validate input is not null, and matches the pattern
                            while (op == null || !Regex.IsMatch(op, @"[\+|\-|\*|/|^|r|s|c|t]"))
                            {
                                Console.WriteLine("Error: Unrecognized input.");
                            }
                            op.Trim().Substring(0, 1);

                            double cleanNum2 = 0;

                            if (Regex.IsMatch(op, @"[\+|\-|/|\*|\^]"))
                            {
                                // Ask the user to type the second number.
                                Console.Write("Type another number, and then press Enter: ");
                                numInput2 = Console.ReadLine();
                                while (!double.TryParse(numInput2, out cleanNum2))
                                {
                                    Console.Write("This is not valid input. Please enter an integer value: ");
                                    numInput2 = Console.ReadLine();
                                }
                            }

                            try
                            {
                                result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                                calculatorUsed++;
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
                            Console.WriteLine($"Calculator was used {calculatorUsed} times");
                            Console.WriteLine("Press 'x' to discard previous result and start new operaion: ");
                            Console.Write("Press 'n' and Enter to go to menu, or press any other key and Enter to resume working on previous result: ");

                            string? endOption = Console.ReadLine();
                            if (endOption == "n")
                            {
                                endApp = true;
                            }
                            else if (endOption != "x")
                            {
                                usePreviousResult = true;
                            }
                            else
                            {
                                usePreviousResult = false;
                            }

                            Console.WriteLine("\n");
                            
                        }
                        calculator.Finish();
                        break;
                    case "2":
                        Console.Clear();
                        if (calculator.ResultList.Count == 0)
                        {
                            Console.WriteLine("-- Currently there is no calculation History --");
                        }
                        else
                        {
                            Console.WriteLine("-- All the calcualtion history ---\n");
                            foreach (var result in calculator.ResultList)
                            {
                                result.Display();
                            }
                        }

                        Console.ReadLine();
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("Are you sure you want to delete calculation record (y/n)?");

                        string? answer = Console.ReadLine();

                        while (answer == null || (answer != "y" && answer != "n"))
                        {
                            Console.WriteLine("Please give the valid value (y/n)!!");
                            answer = Console.ReadLine();
                        }

                        if (answer == "y")
                        {
                            calculator.ResultList.Clear();
                            Console.Clear();
                            Console.WriteLine("All the Calculation History Deleted! (Press Enter)");
                            Console.ReadLine();
                        }
                        break;

                }
            } while (userInput != "4");
        }
    }
}
/*internal class Program
{
    static void Main(string[] args)
    {
        string? userInput; //? used to for nullable userinput 

        CalculatorLibrary.Calculator calculator = new CalculatorLibrary.Calculator();
        do
        {
            bool endApp = false;
            int calculatorUsed = 0;
            Console.Clear();
            Console.WriteLine("=====\tCalculator Menu Options\t=====");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Press '1' for calculation operations.");
            Console.WriteLine("Press '2' for viewing and using calculations records.");
            Console.WriteLine("Press '3' for deleting calculation records.");
            Console.WriteLine("Press '4' for existing the program.");

            userInput = Console.ReadLine();
            //Checking the userinput value to be numeric values
            while(userInput == null || !Regex.IsMatch(userInput, "[1-4]"))
            {
                Console.WriteLine("Please enter the value from 1 to 4");
                userInput = Console.ReadLine();
            }

            //using switch statement
            switch(userInput)
            {
                case "1":
                    bool usePreviousResult = false;
                    while(!endApp)
                    {
                        Console.Clear();
                        Console.WriteLine("Calculator using C# console app\r");
                        Console.WriteLine("--------------------------------\n");

                        string? numberInput1 = " ";
                        string? numberInput2 = " ";
                        double result = 0;
                        //asking the user to enter the first number
                        double cleanNum1 = 0;
                        if(!usePreviousResult)
                        {
                            Console.WriteLine("Enter the first number: ");
                            numberInput1 = Console.ReadLine();
                            //chech the entered value and trypars it to double
                            while(!double.TryParse(numberInput1, out cleanNum1))
                                {
                                Console.WriteLine("Invalid value. Please type a numeric value and press enter");
                                numberInput1 = Console.ReadLine();
                            }
                        }

                        //choosing operator
                        Console.WriteLine("Choose an operator: ");
                        Console.WriteLine("\t+ - Add\n\t- - Subtract\n\t* - Multiply\n\t/ - Divide\n\t^ - Power\n\tr - Square root\n\tS - Sin\n\tC - Cosine\n\tT - Tangent");
                        string? op = Console.ReadLine();
                        // checking the validity of operator
                        while(op ==  null || !Regex.IsMatch(op, @"[\+|\-|\*|/|^|r|s|c|t]"))
                        {
                            Console.WriteLine("Error: Unrecognized operator. Please try again.");
                            op = Console.ReadLine();
                        }
                        op = op.Trim().Substring(0, 1);

                        //asking for the second number to be entered
                        double cleanNum2 = 0;
                        if(Regex.IsMatch(op, @"[\+|\-|\*|/|\^]"))
                        {
                            Console.WriteLine("Enter the second number: ");
                            numberInput2 = Console.ReadLine();

                            while(!double.TryParse(numberInput2, out cleanNum2))
                            {
                                Console.WriteLine("Invalid input. Please type a numeric value.");
                                numberInput2 = Console.ReadLine();
                            }
                        }
                        //choosing the operator
                        try
                        {
                            result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                            calculatorUsed++;
                            if(double.IsNaN(result))
                            {
                                Console.WriteLine("This operation will result in a mathematical error.");
                            }
                            else
                            {
                                Console.WriteLine("Result: {0:0.##}\n", result);
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("Oh no!!! An exception occured trying to do mathematical operation.\n Details: ", e.Message);
                        }

                        Console.WriteLine("-------------------------------------");
                        Console.WriteLine($"Calculator was used {calculatorUsed} times.");
                        Console.WriteLine("Press 'x' to discard previous result and start new operation: ");
                        Console.WriteLine("Press 'n' and enter to go to main menue, or press any other key and press enter to resume working on the previous results: ");

                        string? endOption = Console.ReadLine();
                        if(endOption == "n")
                        {
                            endApp = true;
                        }
                        else if(endOption != "x")
                        {
                            usePreviousResult = true;
                        }
                        else { usePreviousResult = false; }
                        Console.WriteLine("\n");
                    }
                    calculator.Finish();
                    break;
                case "2":
                    Console.Clear();
                    if(calculator.ResultList.Count == 0)
                    {
                        Console.WriteLine("---Currently there is no calculation records---");
                    }
                    else
                    {
                        Console.WriteLine("-----All the calculation records-----\n");
                        foreach(var result in calculator.ResultList)
                        {
                            result.Display();
                        }
                    }
                    Console.ReadLine();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Are you sure you want to delete all the calculation records? (Y/N)");
                    string? userAnswer = Console.ReadLine();

                    while(userAnswer == null || (userAnswer != "y" && userAnswer != "n"))
                    {
                        Console.WriteLine("Please give the correct value.(Y/N)");
                        userAnswer = Console.ReadLine();
                    }
                    if(userAnswer == "y")
                    {
                        calculator.ResultList.Clear();
                        Console.Clear();
                        Console.WriteLine("All the calculation records has been deleted. Press enter to continue");
                        Console.ReadLine();
                    }
                    break;


            }
        }while(userInput != "4");
    }
}*/

