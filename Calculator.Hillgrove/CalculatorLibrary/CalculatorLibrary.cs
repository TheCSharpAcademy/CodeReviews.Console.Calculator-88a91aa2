using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        private List<string> _calculationHistory = new List<string>();

        public int UsageCount { get; private set; }

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
            UsageCount++;
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            string operationString = "";

            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    operationString = $"{num1} + {num2} = {result:N2}";
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    operationString = $"{num1} - {num2} = {result:N2}";
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    operationString = $"{num1} * {num2} = {result:N2}";
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    operationString =
                        num2 != 0 ? $"{num1} / {num2} = {result:N2}" : $"{num1} / {num2} = Error";
                    break;
                default:
                    operationString = "Invalid operation";
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            _calculationHistory.Add(operationString);
            return result;
        }

        public void ClearHistory()
        {
            _calculationHistory.Clear();
        }

        public List<string> GetHistory()
        {
            return [.. _calculationHistory];
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}
