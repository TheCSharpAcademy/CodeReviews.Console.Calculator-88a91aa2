using Newtonsoft.Json;
using System.Text.RegularExpressions;
using CalculatorLibrary.Models;

namespace CalculatorLibrary
{
    
    public class Calculator
    {
        internal static List<CalculationsHistory> history = new List<CalculationsHistory> { };
        JsonWriter writer;
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }
        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN;
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    AddToHistory($"{num1} + {num2}",result);
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    AddToHistory($"{num1} - {num2}", result);
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    AddToHistory($"{num1} * {num2}", result);
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        AddToHistory($"{num1} / {num2}", result);
                    }
                    writer.WriteValue("Divide"); 
                    break;
                case "r":
                    if (num1 >= 0)
                    {
                        result = Math.Sqrt(num1);
                        AddToHistory($"{num1} / {num2}", result);
                    }
                    writer.WriteValue("Square Root");
                    break;
                case "e":
                    result = Math.Pow(num1,num2);
                    writer.WriteValue("Exponentiation");
                    AddToHistory($"{num1}^{num2}", result);
                    break;
                case "i":
                    result = Math.Sin(num1);
                    writer.WriteValue("Sine");
                    AddToHistory( $"sin({num1})", result);
                    break;
                case "c":
                    result = Math.Cos(num1);
                    writer.WriteValue("Cosine");
                    AddToHistory($"cos({num1})", result);
                    break;
                case "t":
                    result = Math.Tan(num1);
                    writer.WriteValue("Tangent ");
                    AddToHistory($"tg({num1})", result);
                    break;
                case "x":
                    result = num1 * 10;
                    writer.WriteValue("10x ");
                    AddToHistory($"10*{num1}", result);
                    break;
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            return result;
        }
        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public void AddToHistory(string operation, double result)
        {
            history.Add(new CalculationsHistory
            {
                
                operation = operation,
                result = result
            });
        }

        public void CheckHistory()
        {
            Console.Clear();
            Console.WriteLine("Calculations History");
            Console.WriteLine("------------------------\n");
            int counter = 0;
            foreach (var operation in history)
            {
                Console.WriteLine($"{++counter}. {operation.operation} = {operation.result}");
            }
            Console.WriteLine("------------------------\n");

            bool endSession = false;
            string? input;
            do
            {
                Console.Write($@"Press '0' and Enter to return, press 'd' to delete history and return, 
or press number of calculation from history to use it's result for the next calculation: ");
                input = Console.ReadLine();
                int number = -1;
                bool isNumber = Int32.TryParse(input, out number);
                if (isNumber && number >= 1 && number <= counter)
                {
                    double num1 = history[number-1].result;
                    Console.WriteLine($"Your first number is {num1}\n");
                    Console.WriteLine("Choose an operator from the following list:");
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
                    if (op == null || !Regex.IsMatch(op, "[a|s|m|d|r|e|i|c|t|x]"))
                    {
                        Console.WriteLine("Error: Unrecognized input.");
                    }
                    else
                    {
                        string numInput2;
                        double num2 = 0;
                        if (Regex.IsMatch(op, "[a|s|m|d|e]"))
                        {
                            Console.Write("Type another number, and then press Enter: ");
                            numInput2 = Console.ReadLine();

                            while (!double.TryParse(numInput2, out num2))
                            {
                                Console.Write("This is not valid input. Please enter a numeric value: ");
                                numInput2 = Console.ReadLine();
                            }
                        }
                        try
                        {
                            double result = DoOperation(num1, num2, op);
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
                    endSession = true;
                }
                else if (input == "d") history.Clear();

                else if (input != "0")
                {
                    Console.WriteLine("Error: Unrecognized input.\n");
                }
            }while ( !endSession && input!="0" && input !="d");
        }
    }    
}
