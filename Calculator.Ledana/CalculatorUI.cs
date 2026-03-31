using CalculatorLibrary.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalculatorConsole.Ledana
{
    public class CalculatorUI
    {
        //int count = 0;

        public bool ShouldContinue()
        {
            Console.WriteLine("Do you want to use the calculator or close it? Press 'x' and enter to close or enter to continue");
            string? input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                if (input.ToLower() == "x")
                    return false;
            }
            return true;
        }
        public bool HandleHistory(Calculator calculator, ref int count)
        {
            string? input = "";
            //int count = calculator.Results.Keys.Max() + 1;
            calculator.GetLatestCalculation();
            Console.WriteLine("Do you want to delete the list? 'y' or 'n'");
            input = Console.ReadLine();
            if (input == "y")
            {
                calculator.ClearCalculations();
                Console.WriteLine("List of calculations is cleared succesfully");
            }
            //if not we ask if they want to use the Results only if the list has 2 or more calulations
            else if (calculator.Calculations.Count >= 2)
            {
                Console.WriteLine("Do you want to use Results of old Calculations or enter new numbers? Type 'r' or 'n'");
                input = Console.ReadLine();
                //if yes we ask each number they want and assign them in cleanNum1 and cleanNum2
                if (input == "r")
                {
                    foreach (var item in calculator.Results)
                    {
                        Console.WriteLine(item.Key + " : " + item.Value);
                    }

                    int choice = (int)GetNumber("Please choose the number from results");
                    while (!(choice >= calculator.Results.Keys.Min() && choice <= calculator.Results.Keys.Max()))
                    {
                        choice = (int)GetNumber("Type a number within the results");
                    }
                    double cleanNum1 = calculator.Results[choice];

                    // Ask the user to choose an option
                    string? op = GetOperation();

                    if (HandleSingleOrTrig(calculator, cleanNum1, op, ref count))
                        return true;

                    Console.WriteLine("Choose the other result you want to use");

                    choice = (int)GetNumber("Please put a number");
                    while (!(choice >= calculator.Results.Keys.Min() && choice <= calculator.Results.Keys.Max()))
                    {
                        choice = (int)GetNumber("Type a number within the results");
                    }
                    double cleanNum2 = calculator.Results[choice];

                    double result = calculator.DoOperation(cleanNum1, op, cleanNum2);
                    PrintResult(result, ref count);
                    //we make the flag true so we skip assigning cleanNum1 and cleanNum2 again
                    return true;
                }
            }
            return false;
        }
        public double GetNumber(string message)
        {
            Console.WriteLine(message);
            double num = 0;
            string? input = Console.ReadLine();
            while (!double.TryParse(input, out num))
            {
                Console.WriteLine(message);
                input = Console.ReadLine();
            }
            return num;
        }
        public string? GetOperation()
        {
            ChooseOption();
            string? op = Console.ReadLine();
            while (op == null || !Regex.IsMatch(op, @"^(?:\+|\-|\*|/|s|p|10x|t)$"))
            {
                Console.WriteLine("Please put a valid operation");
                op = Console.ReadLine();
            }
            return op;
        }
        public bool HandleSingleOrTrig(Calculator calculator, double cleanNum1, string? op, ref int count)
        {
            double result;
            if (op == "s" || op == "10x" || op == "t")
            {
                switch (op)
                {
                    case "s":
                    case "10x":
                        result = calculator.DoOperation(cleanNum1, op);
                        if (double.IsNaN(result))
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        else
                        {
                            PrintResult(result, ref count);
                        }
                        break;
                    case "t":
                        result = UserChoosesTrigonemetry(calculator, cleanNum1);
                        if (double.IsNaN(result))
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        else
                        {
                            PrintResult(result, ref count);
                        }
                        break;
                }
                return true;
            }
            else
                return false;
        }
        public void PrintResult(double result, ref int count)
        {
            Console.WriteLine($"Your result: {result}");
            count++;
            Console.WriteLine($"The calculator is used {count} times");
            Console.WriteLine("------------------------\n");
        }

        public double UserChoosesTrigonemetry(Calculator calculator, double cleanNum1)
        {
            Console.WriteLine("Degree or Radian: 'd' or 'r'");
            string? input = Console.ReadLine();
            while (input == null || !Regex.IsMatch(input, @"^(d|r)$"))
            {
                Console.WriteLine("Please put a valit operation");
                input = Console.ReadLine();
            }

            if (input == "d")
                cleanNum1 = calculator.DegreeToRadius(cleanNum1);

            ChooseTrigonometryOptions();
            string? op = Console.ReadLine();
            while (op == null || !Regex.IsMatch(op, @"^(sin|cos|tan|sec|cosec|cot)$"))
            {
                Console.WriteLine("Please choose a valid operation");
                op = Console.ReadLine();
            }

            double result = calculator.Trigonometry(cleanNum1, op);
            return result;
        }
        public void ChooseOption()
        {
            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\t+ - Add");
            Console.WriteLine("\t- - Subtract");
            Console.WriteLine("\t* - Multiply");
            Console.WriteLine("\t/ - Divide");
            Console.WriteLine("\ts - Square Root");
            Console.WriteLine("\tp - Taking the Power");
            Console.WriteLine("\t10x - Multiply 10 times");
            Console.WriteLine("\tt - Trigonometry");
            Console.Write("Your option? ");
        }

        public void ChooseTrigonometryOptions()
        {
            Console.WriteLine("Choose trigonometric operation");
            Console.WriteLine("\tsin - Sin");
            Console.WriteLine("\tcos - Cos");
            Console.WriteLine("\ttan - Tan");
            Console.WriteLine("\tsec - Sec");
            Console.WriteLine("\tcosec - Cosec");
            Console.WriteLine("\tcot - Cot");
        }

        public void ValidateInput(string? op)
        {
            while (op == null || !Regex.IsMatch(op, @"^(?:\+|\-|\*|/|s|p|10x|t)$"))
            {
                Console.WriteLine("Please put a valid operation");
                op = Console.ReadLine();
            }
        }
    }
}
