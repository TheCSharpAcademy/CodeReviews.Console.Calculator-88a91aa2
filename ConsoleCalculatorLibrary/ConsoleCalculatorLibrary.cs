using System.Diagnostics;
using Newtonsoft.Json;

namespace ConsoleCalculatorLibrary;

public class ConsoleCalculator
{
    JsonWriter writer;
    public ConsoleCalculator()
    {
        StreamWriter logFile = File.CreateText("calculator.log");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("operations");
        writer.WriteStartArray();
    }
    
    // CalculatorLibrary.cs
    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
                
        void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                Trace.WriteLine(String.Format("{0} + {1} = {2}", num1, num2, result));
                break;
            case "s":
                result = num1 - num2;
                Trace.WriteLine(String.Format("{0} - {1} = {2}", num1, num2, result));
                break;
            case "m":
                result = num1 * num2;
                Trace.WriteLine(String.Format("{0} * {1} = {2}", num1, num2, result));
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / 0;
                    
                    Trace.WriteLine(String.Format("{0} / {1} = {2}", num1, num2, result));
                }
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();
        
        Finish();
        return result;
    }
}