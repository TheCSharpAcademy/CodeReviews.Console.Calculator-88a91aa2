/// <summary>
/// This class handles arithmetic calculations and returns the result.
/// </summary>

using Newtonsoft.Json;

namespace OperationsLibrary
{
    public class Operations
    {
        JsonWriter writer;

        public Operations()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public static double GetUserNumericValue()
        {
            Console.Write("\nType a number, and then press Enter: ");
            string? userInput = Console.ReadLine();

            double numericValue = 0;

            while (!double.TryParse(userInput, out numericValue))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                userInput = Console.ReadLine();
            }

            return numericValue;
        }

        public double BasicArithmeticOperations(double numericValueOne, double numericValueTwo, string operationType)
        {
            double result = double.NaN;

            writer.WriteStartObject();
            writer.WritePropertyName("Operand one");
            writer.WriteValue(numericValueOne);
            writer.WritePropertyName("Operand two");
            writer.WriteValue(numericValueTwo);
            writer.WritePropertyName("Operation");

            switch (operationType)
            {
                case "1":
                    result = numericValueOne + numericValueTwo;
                    writer.WriteValue("Add");
                    break;

                case "2":
                    result = numericValueOne - numericValueTwo;
                    writer.WriteValue("Subtract");
                    break;

                case "3":
                    result = numericValueOne * numericValueTwo;
                    writer.WriteValue("Multiply");
                    break;

                case "4":
                    if (numericValueTwo != 0)
                    {
                        result = numericValueOne / numericValueTwo;
                    }
                    writer.WriteValue("Divide");
                    break;

                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }
        public double ExponentsAndRadicals(double numericValue, string operationType)
        {
            double result = double.NaN;

            writer.WriteStartObject();
            writer.WritePropertyName("Operand one");
            writer.WriteValue(numericValue);
            writer.WritePropertyName("Operation");

            switch (operationType)
            {
                case "1":
                    result = Math.Sqrt(numericValue);
                    writer.WriteValue("Square root");
                    break;

                case "2":
                    result = Math.Pow(numericValue, 2);
                    writer.WriteValue("Power of two");
                    break;

                case "3":
                    result = Math.Pow(10, numericValue);
                    writer.WriteValue("Exponentiation");
                    break;

                default:
                    break;
            }

            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public double TrigonometricFunctions(double numericValue, string operationType)
        {
            double result = double.NaN;

            writer.WriteStartObject();
            writer.WritePropertyName("Operand one");
            writer.WriteValue(numericValue);
            writer.WritePropertyName("Operation");

            switch (operationType)
            {
                case "1":
                    result = Math.Sin((numericValue * Math.PI / 180));
                    writer.WriteValue("Sine");
                    break;

                case "2":
                    result = Math.Cos((numericValue * Math.PI / 180));
                    writer.WriteValue("Cosine");
                    break;

                case "3":
                    result = Math.Tan((numericValue * Math.PI / 180));
                    writer.WriteValue("Tangent");
                    break;

                case "4":
                    result = 1 / Math.Sin((numericValue * Math.PI / 180));
                    writer.WriteValue("Consecant");
                    break;

                case "5":
                    result = 1 / Math.Cos((numericValue * Math.PI / 180));
                    writer.WriteValue("Secant");
                    break;

                case "6":
                    result = 1 / Math.Tan((numericValue * Math.PI / 180));
                    writer.WriteValue("Cotangent");
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
    }
}