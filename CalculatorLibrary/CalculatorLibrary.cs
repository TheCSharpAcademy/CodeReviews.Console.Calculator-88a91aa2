// CalculatorLibrary.cs
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {

        JsonWriter writer;
        int usedCount;
        List<double> results;

        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();

            usedCount = 0;
            results = [];
        }

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Taking the Power");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            usedCount++;
            results.Add(result);

            return result;
        }

        public double DoOperation(double num, string op)
        {

            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand");
            writer.WriteValue(num);
            writer.WritePropertyName("Operation");

            switch (op)
            {
                case "r":
                    result = Math.Sqrt(num);
                    writer.WriteValue("Square root");
                    break;
                case "t":
                    result = 10 * num;
                    writer.WriteValue("10x");
                    break;
                case "x":
                    result = Math.Sin(num);
                    writer.WriteValue("Sine");
                    break;
                case "y":
                    result = Math.Cos(num);
                    writer.WriteValue("Cosine");
                    break;
                case "z":
                    result = Math.Tan(num);
                    writer.WriteValue("Tangent");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            usedCount++;
            results.Add(result);

            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public int DisplayUsedCount()
        {
            return usedCount;
        }

        public void ClearResultList()
        {
            if (results.Count > 0)
            {
                results.Clear();
            }
        }

        public List<double> GetResultList()
        {
            return results;
        }
    }
}
