using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        //Initiating a list to store calculation records
        public List<Result> ResultList = new List<Result>();

        JsonWriter writer;
        //Method to create a file and start writing calculator records to that file.
        public Calculator()
        {
            //Creating log file
            StreamWriter logFile = File.CreateText("CalculatorLog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            //Starting to write
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public double DoCalculation(double number1, double number2, string op)
        {
            double result = double.NaN;
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(number1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(number2);
            writer.WritePropertyName("Operations");

            switch (op)
            {
                case "+":
                    result = number1 + number2;
                    ResultList.Add(new Result
                    {
                        Operand1 = number1,
                        Operand2 = number2,
                        Operation = op,
                        Answer = result
                    });
                    writer.WriteValue("Add");
                    break;
                case "-":
                    result = number1 - number2;
                    ResultList.Add(new Result
                    {
                        Operand1 = number1,
                        Operand2 = number2,
                        Operation = op,
                        Answer = result
                    });
                    writer.WriteValue("Subtract");
                    break;
                case "*":
                    result = number1 * number2;
                    ResultList.Add(new Result
                    {
                        Operand1 = number1,
                        Operand2 = number2,
                        Operation = op,
                        Answer = result
                    });
                    writer.WriteValue("Multiply");
                    break;
                case "/":
                    if(number2 != 0 )
                    {
                        result = number1 / number2;
                    }
                    
                    ResultList.Add(new Result
                    {
                        Operand1 = number1,
                        Operand2 = number2,
                        Operation = op,
                        Answer = result
                    });
                    writer.WriteValue("Divide");
                    break;
                case "^":
                    result = Math.Pow(number1, number2);
                    ResultList.Add(new Result
                    {
                        Operand1 = number1,
                        Operand2 = number2,
                        Operation = op,
                        Answer = result
                    });
                    writer.WriteValue("Power");
                    break;
                case "r":
                    result = Math.Sqrt(number1);
                    ResultList.Add(new Result
                    {
                        Operand1 = number1,
                        Operation = op,
                        Answer = result
                    });
                    writer.WriteValue("Square root");
                    break;
                case "s":
                    result = Math.Sin(number1);
                    ResultList.Add(new Result
                    {
                        Operand1 = number1,
                        Operation = op,
                        Answer = result
                    });
                    writer.WriteValue("Sin");
                    break;
                case "c":
                    result = Math.Cos(number1);
                    ResultList.Add(new Result
                    {
                        Operand1 = number1,
                        Operation = op,
                        Answer = result
                    });
                    writer.WriteValue("Cos");
                    break;
                case "t":
                    result = Math.Tan(number1);
                    ResultList.Add(new Result
                    {
                        Operand1 = number1,
                        Operation = op,
                        Answer = result
                    });
                    writer.WriteValue("Tangant");
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
