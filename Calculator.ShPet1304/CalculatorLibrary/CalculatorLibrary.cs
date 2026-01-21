using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        private static Calculator? _instance;
        private static List<(double num1, double num2, double result)> history = new();
        private JsonWriter writer;
        private StreamWriter logFile;

        public static Calculator Instance
        {
            get
            {
                _instance ??= new Calculator();
                return _instance;
            }
        }

        private Calculator()
        {
            logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public double DoOperation(double num1 = 0, double num2 = 0, string op = "")
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
                case "addition":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    history.Add(((double)num1, (double)num2, (double)result));
                    break;

                case "subtraction":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    history.Add(((double)num1, (double)num2, (double)result));
                    break;

                case "multiplication":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    history.Add(((double)num1, (double)num2, (double)result));
                    break;

                case "division":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    history.Add(((double)num1, (double)num2, (double)result));
                    break;

                case "Square Root":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square Root");
                    history.Add(((double)num1, (double)num2, (double)result));
                    break;

                case "Taking the power":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Taking the power");
                    history.Add(((double)num1, (double)num2, (double)result));
                    break;

                case "10x":
                    result = Math.Pow(10, num1);
                    writer.WriteValue("10x");
                    history.Add(((double)num1, (double)num2, (double)result));

                    break;

                case "Trigonometric function (sin)":
                    double radians = num1 * (Math.PI / 180);
                    result = Math.Sin(radians);
                    writer.WriteValue("Sine");
                    history.Add(((double)num1, (double)num2, (double)result));
                    break;

                case "Trigonometric function (cos)":
                    double radiansCos = num1 * (Math.PI / 180);
                    result = Math.Cos(radiansCos);
                    writer.WriteValue("Cosine");
                    history.Add(((double)num1, (double)num2, (double)result));
                    break;

                case "Trigonometric function (tan)":
                    double radiansTan = num1 * (Math.PI / 180);
                    result = Math.Tan(radiansTan);
                    writer.WriteValue("Tangent");
                    history.Add(((double)num1, (double)num2, (double)result));
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

        public void GetLastResult()
        {
            if (history == null || history.Count == 0)
            {
                Console.WriteLine("No history found.");
            }
            else
            {
                foreach (var entry in history)
                {
                    Console.WriteLine($"{entry.result}");
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public List<double> PreviousResult()
        {
            var previousResultList = new List<double>();
            if (history == null || history.Count == 0)
            {
                return previousResultList;
            }
            else
            {
                foreach (var entry in history)
                {
                    previousResultList.Add(entry.result);
                }
                return previousResultList;
            }
        }

        public void GetUsageCount()
        {
            int historyCount = 0;
            int count = 0;
            if (history == null || history.Count == 0)
            {
                Console.WriteLine("No history count found.");
            }
            else
            {
                foreach (var entry in history)
                {
                    count++;
                }
                Console.WriteLine($"Calculator Used: {historyCount += count} times");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void ClearHistory()
        {
            try
            {
                if (Calculator.history == null)
                {
                    Console.WriteLine("Error: History list was never initialized!");
                    return;
                }
                Calculator.history.Clear();
                Console.WriteLine($"History count is now: {Calculator.history.Count}");
                Console.WriteLine("History Cleared. Press any key to continue...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Console.ReadKey();
        }
    }
}


