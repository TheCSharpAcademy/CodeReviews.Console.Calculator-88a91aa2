using Newtonsoft.Json;


namespace CalculatorLibrary
{

  public class Calculator
  {
    public int usageCount;
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

    public double DoOperation(double num1, double num2, char op)
    {
      double result = double.NaN;

      writer.WriteStartObject();
      writer.WritePropertyName("Operand1");
      writer.WriteValue(num1);
      writer.WritePropertyName("Operand2");
      writer.WriteValue(num2);
      writer.WritePropertyName("Operation");

      // Use a switch statement to do the math.
      switch (char.ToLower(op))
      {
        case 'a':
          result = num1 + num2;
          writer.WriteValue("Add");
          break;
        case 's':
          result = num1 - num2;
          writer.WriteValue("Subtract");
          break;
        case 'm':
          result = num1 * num2;
          writer.WriteValue("Multiply");
          break;
        case 'd':
          // Ask the user to enter a non-zero divisor.
          if (num2 != 0)
          {
            result = num1 / num2;
          }
          writer.WriteValue("Divide");
          break;
        case 'e':
          writer.WriteValue("Exit");
          break;
        default:
          writer.WriteValue("Invalid Operation");
          break;
      }
      writer.WritePropertyName("Result");
      writer.WriteValue(result);
      writer.WriteEndObject();

      usageCount++;
      return result;
    }

    // Add a method to close the JSON writer
    public void Close()
    {
      writer.WriteEndArray();
      writer.WriteEndObject();
      writer.Close();
    }
  }

}
