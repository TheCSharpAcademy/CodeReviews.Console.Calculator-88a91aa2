using MemoryLibrary;
using OperationsLibrary;
using System.Text.RegularExpressions;

/// <summary>
/// This class manages the program's execution flow.
/// </summary>

class Calculator
{
    static void Main(string[] args)
    {
        bool isEndApp = false;
        int numberOfOperations = 0;

        Dictionary<int, string[]> listOfOperations = [];
        List<string> info = [];

        Operations calculator = new();

        while (!isEndApp)
        {
            Console.WriteLine("Console Calculator in C# - Main Menu\r");
            Console.WriteLine("------------------------------------\n");

            Console.WriteLine("Choose an math category from the following list:\n");

            Console.WriteLine("\t1 - Arithmetic Operations");
            Console.WriteLine("\t2 - Exponents and Radicals");
            Console.WriteLine("\t3 - Trigonometric Functions");

            Console.WriteLine("\t4 - List of operations performed");

            Console.WriteLine($"\nNumber of times calculator was used: {numberOfOperations}");

            Console.Write("\nWhat's your option? ");

            string? userInput = Console.ReadLine();

            if (userInput == null || !Regex.IsMatch(userInput.Trim(), "^[1-4]$"))
            {
                Console.WriteLine("\nError: Unrecognized math category input.");
            }
            else
            {
                switch (userInput)
                {
                    case "1":
                        Console.Clear();

                        Console.WriteLine("Console Calculator in C# - Basic Arithmetic Operations\r");
                        Console.WriteLine("------------------------------------------------------\n");

                        Console.WriteLine("Choose an math operation from the following list:\n");

                        Console.WriteLine("\t1 - Add");
                        Console.WriteLine("\t2 - Subtract");
                        Console.WriteLine("\t3 - Multiply");
                        Console.WriteLine("\t4 - Divide");

                        Console.WriteLine($"\nNumber of times calculator was used: {numberOfOperations}");

                        Console.Write("\nWhat's your option? ");

                        userInput = Console.ReadLine();

                        if (userInput == null || !Regex.IsMatch(userInput.Trim(), "^[1-4]$"))
                        {
                            Console.WriteLine("\nError: Unrecognized input.");
                            break;
                        }
                        else
                        {
                            double cleanNum1 = 0;
                            double cleanNum2 = 0;
                            double result = 0;

                            if (listOfOperations.Count != 0)
                            {
                                (cleanNum1, cleanNum2) = Memory.GetValuesFromList(listOfOperations);
                                if (cleanNum1 == 0 && cleanNum2 == 0) break;
                            }
                            else
                            {
                                cleanNum1 = Operations.GetUserNumericValue();
                                cleanNum2 = Operations.GetUserNumericValue();
                            }

                            try
                            {
                                result = calculator.BasicArithmeticOperations(cleanNum1, cleanNum2, userInput);

                                if (double.IsNaN(result))
                                {
                                    Console.WriteLine("This operation will result in a mathematical error.\n");
                                }
                                else
                                {
                                    Console.WriteLine("\nYour result: {0:0.##}", result);
                                    numberOfOperations++;
                                    info = [result.ToString(), Memory.GetOperationName(int.Parse(userInput))];
                                    listOfOperations.Add(numberOfOperations, [.. info]);
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("\nOh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                            }
                        }

                        break;

                    case "2":
                        Console.Clear();

                        Console.WriteLine("Console Calculator in C# - Exponents and Radicals Operations\r");
                        Console.WriteLine("------------------------------------------------------------\n");

                        Console.WriteLine("Choose an math operation from the following list:\n");

                        Console.WriteLine("\t1 - Square Root");
                        Console.WriteLine("\t2 - Power of two");
                        Console.WriteLine("\t3 - Exponentiation");

                        Console.WriteLine($"\nNumber of times calculator was used: {numberOfOperations}");

                        Console.Write("\nWhat's your option? ");

                        userInput = Console.ReadLine();

                        if (userInput == null || !Regex.IsMatch(userInput.Trim(), "^[1-3]$"))
                        {
                            Console.WriteLine("\nError: Unrecognized math category input.");
                        }
                        else
                        {
                            double cleanNum1 = 0;
                            double result = 0;

                            if (listOfOperations.Count != 0)
                            {
                                cleanNum1 = Memory.GetValueFromList(listOfOperations);
                                if (cleanNum1 == 0) break;
                            }
                            else
                            {
                                cleanNum1 = Operations.GetUserNumericValue();
                            }

                            try
                            {
                                result = calculator.ExponentsAndRadicals(cleanNum1, userInput);

                                if (double.IsNaN(result))
                                {
                                    Console.WriteLine("This operation will result in a mathematical error.\n");
                                }
                                else
                                {
                                    Console.WriteLine("\nYour result: {0:0.##}", result);
                                    numberOfOperations++;
                                    info = [result.ToString(), Memory.GetOperationName(int.Parse(userInput) + 4)];
                                    listOfOperations.Add(numberOfOperations, [.. info]);
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("\nOh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                            }
                        }

                        break;

                    case "3":
                        Console.Clear();

                        Console.WriteLine("Console Calculator in C# - Trigonometric Functions\r");
                        Console.WriteLine("--------------------------------------------------\n");

                        Console.WriteLine("Choose an math operation from the following list:\n");

                        Console.WriteLine("\t1 - Sine");
                        Console.WriteLine("\t2 - Cosine");
                        Console.WriteLine("\t3 - Tangent");
                        Console.WriteLine("\t4 - Consecant");
                        Console.WriteLine("\t5 - Secant");
                        Console.WriteLine("\t6 - Cotangent");

                        Console.WriteLine($"\nNumber of times calculator was used: {numberOfOperations}");

                        Console.Write("\nWhat's your option? ");

                        userInput = Console.ReadLine();

                        if (userInput == null || !Regex.IsMatch(userInput.Trim(), "^[1-6]$"))
                        {
                            Console.WriteLine("\nError: Unrecognized math category input.");
                        }
                        else
                        {
                            double cleanNum1 = 0;
                            double result = 0;

                            if (listOfOperations.Count != 0)
                            {
                                cleanNum1 = Memory.GetValueFromList(listOfOperations);
                                if (cleanNum1 == 0) break;
                            }
                            else
                            {
                                cleanNum1 = Operations.GetUserNumericValue();
                            }

                            try
                            {
                                result = calculator.TrigonometricFunctions(cleanNum1, userInput);
                                if (double.IsNaN(result))
                                {
                                    Console.WriteLine("This operation will result in a mathematical error.\n");
                                }
                                else
                                {
                                    Console.WriteLine("\nYour result: {0:0.##}", result);
                                    numberOfOperations++;
                                    info = [result.ToString(), Memory.GetOperationName(int.Parse(userInput) + 7)];
                                    listOfOperations.Add(numberOfOperations, [.. info]);
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("\nOh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                            }
                        }

                        break;

                    case "4":
                        Console.Clear();

                        Console.WriteLine("Console Calculator in C# - List of operations performed\r");
                        Console.WriteLine("-------------------------------------------------------\n");

                        if (listOfOperations.Count != 0)
                        {
                            foreach (var information in listOfOperations)
                            {
                                Console.WriteLine($" {information.Key} - Operation: {information.Value[1]} \t Result: {information.Value[0]}");
                            }

                            Console.Write($"\nDo you want to erase the list? (y/n) ");
                            userInput = Console.ReadLine();

                            if (userInput == null || !Regex.IsMatch(userInput.Trim(), "^[y|n]$"))
                            {
                                Console.WriteLine("\nInvalid input. Returning to main menu.");
                            }
                            else
                            {
                                if (userInput.Contains('y'))
                                {
                                    listOfOperations.Clear();
                                    numberOfOperations = 0;
                                    Console.WriteLine("\nList of operations was cleared!");
                                }
                                else
                                {
                                    Console.WriteLine("\nYou have cancelled the action!");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("You haven't performed any operations with the calculator yet!");
                        }

                        break;

                    default:
                        break;
                }
            }

            Console.WriteLine("\n------------------------\n");

            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n")
            {
                isEndApp = true;
            }

            Console.Clear();
        }

        calculator.Finish();

        return;
    }
}
