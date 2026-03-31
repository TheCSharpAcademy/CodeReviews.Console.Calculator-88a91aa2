using OperationsLibrary;
using System.Text.RegularExpressions;

namespace MemoryLibrary
{
    public class Memory
    {
        public static string GetOperationName(int value)
        {
            string s = string.Empty;

            switch (value)
            {
                case 1:
                    s = "Addition";
                    break;

                case 2:
                    s = "Subtraction";
                    break;

                case 3:
                    s = "Multiplication";
                    break;

                case 4:
                    s = "Division";
                    break;

                case 5:
                    s = "Square root";
                    break;

                case 6:
                    s = "Power of two";
                    break;

                case 7:
                    s = "Exponentiation";
                    break;

                case 8:
                    s = "Sine";
                    break;

                case 9:
                    s = "Cosine";
                    break;

                case 10:
                    s = "Tangent";
                    break;

                case 11:
                    s = "Consecant";
                    break;

                case 12:
                    s = "Secant";
                    break;

                case 13:
                    s = "Cotangent";
                    break;

                default:
                    break;
            }

            return s;
        }

        public static (double numberOne, double numberTwo) GetValuesFromList(Dictionary<int, string[]> listOfOperations)
        {
            double cleanNum1 = 0d;
            double cleanNum2 = 0d;

            Console.Write("\nDo you want to use previous result? (y/n) ");
            string? userQuery = Console.ReadLine();

            if (userQuery == null || !Regex.IsMatch(userQuery.Trim(), "^[y|n]$"))
            {
                Console.WriteLine("\nError: Unrecognized math category input.");
                return (0d, 0d);
            }
            else if (userQuery.Contains('y'))
            {
                Console.WriteLine("\nGetting value from list!\n");

                foreach (var information in listOfOperations)
                {
                    Console.WriteLine($" {information.Key} - Operation: {information.Value[1]} \t Result: {information.Value[0]}");
                }

                Console.Write("\nWhich result do you want to use? (Type the number from list) ");
                userQuery = Console.ReadLine();

                string pattern = $"^[1-{listOfOperations.Count}]$";

                if (userQuery == null || !Regex.IsMatch(userQuery.Trim(), pattern))
                {
                    Console.WriteLine("\nError: Unrecognized input.");
                    return (0d, 0d);
                }
                else
                {
                    foreach (var information in listOfOperations)
                    {
                        if (information.Key == int.Parse(userQuery))
                        {
                            cleanNum1 = double.Parse(information.Value[0]);
                        }
                    }

                    Console.WriteLine($"\nYou choose the following value to work with: {cleanNum1}");
                }

                cleanNum2 = Operations.GetUserNumericValue();

                return (cleanNum1, cleanNum2);
            }
            else
            {
                cleanNum1 = Operations.GetUserNumericValue();
                cleanNum2 = Operations.GetUserNumericValue();

                return (cleanNum1, cleanNum2);
            }
        }

        public static double GetValueFromList(Dictionary<int, string[]> listOfOperations)
        {
            double cleanNum1 = 0d;

            Console.Write("\nDo you want to use previous result? (y/n) ");
            string? userQuery = Console.ReadLine();

            if (userQuery == null || !Regex.IsMatch(userQuery.Trim(), "^[y|n]$"))
            {
                Console.WriteLine("\nError: Unrecognized math category input.");

                return 0d;
            }
            else if (userQuery.Contains('y'))
            {
                Console.WriteLine("\nGetting value from list!\n");

                foreach (var information in listOfOperations)
                {
                    Console.WriteLine($" {information.Key} - Operation: {information.Value[1]} \t Result: {information.Value[0]}");
                }

                Console.Write("\nWhich result do you want to use? (Type the number from list) ");
                userQuery = Console.ReadLine();

                string pattern = $"^[1-{listOfOperations.Count}]$";

                if (userQuery == null || !Regex.IsMatch(userQuery.Trim(), pattern))
                {
                    Console.WriteLine("\nError: Unrecognized input.");

                    return 0d;
                }
                else
                {
                    foreach (var information in listOfOperations)
                    {
                        if (information.Key == int.Parse(userQuery))
                        {
                            cleanNum1 = double.Parse(information.Value[0]);
                        }
                    }

                    Console.WriteLine($"\nYou choose the following value to work with: {cleanNum1}");
                }

                return cleanNum1;
            }
            else
            {
                return Operations.GetUserNumericValue();
            }
        }
    }
}
