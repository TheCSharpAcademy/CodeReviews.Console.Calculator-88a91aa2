// CalculatorLibrary.cs
using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{

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

    public (double, string) DoOperation(double num1, double num2, string op, bool singleOperand)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        double radians = num1 * Math.PI / 180;
        string operation = "";
        writer.WriteStartObject();
        if (singleOperand)
        {
            writer.WritePropertyName("Operand");
            writer.WriteValue(num1);
        }
        else
        {
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
        }
        writer.WritePropertyName("Operation");

        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                writer.WriteValue("Add");
                operation = $"{num1} + {num2} = {result}";
                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                operation = $"{num1} - {num2} = {result}";
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                operation = $"{num1} * {num2} = {result}";
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                writer.WriteValue("Divide");
                operation = $"{num1} / {num2} = {result}";
                break;
            case "pw":
                result = Math.Pow(num1, num2);
                writer.WriteValue("Taking the power");
                operation = $"{num1} ^ {num2} = {result}";
                break;
            case "sqr":
                result = Math.Sqrt(num1);
                writer.WriteValue("Square root");
                operation = $" √{num1} = {result}";
                break;
            case "10":
                result = 10 * num1;
                writer.WriteValue("Power of ten");
                operation = "Power of ten";
                break;
            case "sin":
                result = Math.Sin(radians);
                writer.WriteValue("Sine");
                operation = $"Sine of {radians} = {result}";
                break;
            case "cos":
                result = Math.Cos(radians);
                writer.WriteValue("Cosine");
                operation = $"Cosine of {radians} = {result}";
                break;
            case "tan":
                result = Math.Tan(radians);
                writer.WriteValue("Tangent");
                operation = $"Tangent of {radians} = {result}";
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        return (result, operation);
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}