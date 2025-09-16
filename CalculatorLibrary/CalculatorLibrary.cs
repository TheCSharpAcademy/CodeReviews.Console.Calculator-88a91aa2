using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorApp.CalculatorLibrary;

public class Calculator
{
    JsonWriter writer;
    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculator.log");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }

    public List<string> history = [];
    int timesUsed = 0;

    public double DoOperation(string op, double num1 = 0, double num2 = 0)
    {
        double result = double.NaN; // Default value is "not-a-number" which we use if an operation, such as division, could result in an error.

        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");

        bool wasUsed = true;

        string operationCharacter = "";
        string resultString = "";
        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                operationCharacter = "+";
                result = num1 + num2;
                resultString = $"{num1} {operationCharacter} {num2} = {result}";
                writer.WriteValue("Add");
                AddToHistory(resultString);
                break;
            case "s":
                operationCharacter = "-";
                result = num1 - num2;
                resultString = $"{num1} {operationCharacter} {num2} = {result}";
                writer.WriteValue("Subtract");
                AddToHistory(resultString);
                break;
            case "m":
                operationCharacter = "*";
                result = num1 * num2;
                resultString = $"{num1} {operationCharacter} {num2} = {result}";
                writer.WriteValue("Multiply");
                AddToHistory(resultString);
                break;
            case "d":
                operationCharacter = "/";
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                    resultString = $"{num1} {operationCharacter} {num2} = {result}";
                    writer.WriteValue("Divide");
                    AddToHistory(resultString);
                }
                break;
            case "sq":
                operationCharacter = "√";
                result = Math.Sqrt(num1);
                resultString = $"{operationCharacter}({num1}) = {result}";
                writer.WriteValue("SquareRoot");
                AddToHistory(resultString);
                break;
            case "p":
                operationCharacter = "^";
                result = Math.Pow(num1, num2);
                resultString = $"{num1} {operationCharacter} {num2} = {result}";
                writer.WriteValue("PowerOf");
                AddToHistory(resultString);
                break;
            case "ten":
                operationCharacter = "* 10";
                result = num1 * 10;
                resultString = $"{num1} {operationCharacter} = {result}";
                writer.WriteValue("10x");
                AddToHistory(resultString);
                break;
            case "sin":
                operationCharacter = "sin";
                result = Math.Sin(num1);
                resultString = $"{operationCharacter}({num1}) = {result}";
                writer.WriteValue("Sine");
                AddToHistory(resultString);
                break;
            case "cos":
                operationCharacter = "cos";
                result = Math.Cos(num1);
                resultString = $"{operationCharacter}({num1}) = {result}";
                writer.WriteValue("Cosine");
                AddToHistory(resultString);
                break;
            case "tan":
                operationCharacter = "tan";
                result = Math.Tan(num1);
                resultString = $"{operationCharacter}({num1}) = {result}";
                writer.WriteValue("Tangent");
                AddToHistory(resultString);
                break;
            default:
                wasUsed = false;
                break;
        }
        if (wasUsed && !Double.IsNaN(result))
        {
            timesUsed++;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();
        return result;
    }

    public void DoCommand(string command)
    {
        switch (command)
        {
            case "h":
                ShowHistory(history);
                Console.WriteLine("");
                break;
            case "del":
                DeleteHistory(history);
                break;
            case "u":
                try
                {
                    double lastResult = GetLastResult(history);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                break;
            case "c":
                int used = GetTimesUsed(timesUsed);
                Console.WriteLine($"Calculator was used {used} times");
                break;
        }
    }

    void AddToHistory(string result)
    {
        history.Add($"{history.Count + 1}. {result}");
    }
    void ShowHistory(List<string> history)
    {
        if (history.Count == 0)
        {
            Console.WriteLine("There are no recorded calculations.");
        }
        else
            foreach (string s in history)
            {
                Console.WriteLine(s);
            }
    }
    void DeleteHistory(List<string> history)
    {
        if (history.Count == 0)
        {
            Console.WriteLine("There are no results to delete.");
        }
        else
        {
            Console.WriteLine("Deleting history...");
            history.Clear();
        }
    }

    int GetTimesUsed(int timesUsed)
    {
        return timesUsed;
    }

    public double GetLastResult(List<string> history)
    {
        if (history.Count == 0) { throw new InvalidOperationException("There are no results in history."); }

        string lastResult = history[history.Count - 1];
        string result = lastResult.Substring(lastResult.IndexOf('=') + 1);
        result = result.Trim();
        return Double.Parse(result);
    }
}