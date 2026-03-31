using Newtonsoft.Json;
using System.Formats.Asn1;

namespace CalculatorLibrary.cs
{
        public class Calculator : IDisposable
        {
            public JsonWriter Writer { get; private set; }
            private StreamWriter logFile;

            public List<string> Calculations { get; private set; } = [];
            public Dictionary<int, double> Results { get; private set; } = [];
            private int count = 0;
            public Calculator()
            {

                logFile = File.CreateText("calculatorlog.json");
                logFile.AutoFlush = true;
                Writer = new JsonTextWriter(logFile);
                Writer.Formatting = Newtonsoft.Json.Formatting.Indented;
                Writer.WriteStartObject();
                Writer.WritePropertyName("Operations");
                Writer.WriteStartArray();
            }
            public double DoOperation(double num1, string? op, double num2 = 0)
            {

                double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
                Writer.WriteStartObject();
                Writer.WritePropertyName("Operand1");
                Writer.WriteValue(num1);
                Writer.WritePropertyName("Operand2");
                Writer.WriteValue(num2);
                Writer.WritePropertyName("Operation");
                // Use a switch statement to do the math.
                switch (op)
                {
                    case "+":
                        result = num1 + num2;
                        Writer.WriteValue("Add");
                        Calculations.Add(string.Format("{0} + {1} = {2}", num1, num2, result));
                        Results.Add(count++, result);
                        break;
                    case "-":
                        result = num1 - num2;
                        Writer.WriteValue("Subtract");
                        Calculations.Add(string.Format("{0} - {1} = {2}", num1, num2, result));
                        Results.Add(count++, result);
                        break;
                    case "*":
                        result = num1 * num2;
                        Writer.WriteValue("Multiply");
                        Calculations.Add(string.Format("{0} * {1} = {2}", num1, num2, result));
                        Results.Add(count++, result);
                        break;
                    case "/":
                        // Ask the user to enter a non-zero divisor.
                        if (num2 != 0)
                        {
                            result = num1 / num2;
                        }
                        Writer.WriteValue("Divide");
                        Calculations.Add(string.Format("{0} / {1} = {2}", num1, num2, result));
                        Results.Add(count++, result);
                        break;
                    case "s":
                        result = Math.Sqrt(num1);
                        Writer.WriteValue("Square Root");
                        Calculations.Add(string.Format("Square Root of {0} = {1}", num1, result));
                        Results.Add(count++, result);
                        break;
                    case "p":
                        result = Math.Pow(num1, num2);
                        Writer.WriteValue("Taking the Power");
                        Calculations.Add(string.Format("{0} taking the power of {1} = {2}", num1, num2, result));
                        Results.Add(count++, result);
                        break;
                    case "10x":
                        result = 10 * num1;
                        Writer.WriteValue("Times 10");
                        Calculations.Add(string.Format("{0} times 10 = {1}", num1, result));
                        Results.Add(count++, result);
                        break;
                    case "t":
                        {

                        }
                        break;
                    // Return text for an incorrect option entry.
                    default:
                        break;
                }
                Writer.WritePropertyName("Result");
                Writer.WriteValue(result);
                Writer.WriteEndObject();

                return result;
            }
            public double DegreeToRadius(double num1)
            {
                return num1 * (Math.PI / 180);
            }
            public double Trigonometry(double num1, string op)
            {
                Writer.WriteStartObject();
                Writer.WritePropertyName("Operand1");
                Writer.WriteValue(num1);
                Writer.WritePropertyName("Operation");
                double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
                switch (op)
                {
                    case "sin":
                        result = Math.Sin(num1);
                        Writer.WriteValue("Sin");
                        Calculations.Add(string.Format("Sin({0}) = {1}", num1, result));
                        Results.Add(count++, result);
                        break;
                    case "cos":
                        result = Math.Cos(num1);
                        Writer.WriteValue("Cos");
                        Calculations.Add(string.Format("Con({0}) = {1}", num1, result));
                        Results.Add(count++, result);
                        break;
                    case "tan":
                        result = Math.Tan(num1);
                        Writer.WriteValue("Tan");
                        Calculations.Add(string.Format("Tan({0}) = {1}", num1, result));
                        Results.Add(count++, result);
                        break;
                    case "sec":
                        result = 1 / Math.Cos(num1);
                        Writer.WriteValue("Sec");
                        Calculations.Add(string.Format("Sec({0}) = {1}", num1, result));
                        Results.Add(count++, result);
                        break;
                    case "cosec":
                        result = 1 / Math.Sin(num1);
                        Writer.WriteValue("Cosec");
                        Calculations.Add(string.Format("Cosec({0}) = {1}", num1, result));
                        Results.Add(count++, result);
                        break;
                    case "cot":
                        result = 1 / Math.Tan(num1);
                        Writer.WriteValue("Cot");
                        Calculations.Add(string.Format("Cot({0}) = {1}", num1, result));
                        Results.Add(count++, result);
                        break;
                }
                Writer.WritePropertyName("Result");
                Writer.WriteValue(result);
                Writer.WriteEndObject();
                return result;
            }
            public void Finish()
            {
                Writer.WriteEndArray();
                Writer.WriteEndObject();
                Writer.Close();
            }
            public void GetLatestCalculation()
            {
                foreach (var item in Calculations)
                {
                    Console.WriteLine(item);
                }

            }
            public void ClearCalculations()
            {
                Calculations.Clear();
                Results.Clear();
            }

            public void Dispose()
            {
                Finish();
                Writer?.Close();
                logFile?.Dispose();
            }
        }
    
}
