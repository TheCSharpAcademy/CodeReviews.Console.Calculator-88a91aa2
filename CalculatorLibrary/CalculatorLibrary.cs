using Newtonsoft.Json;

namespace CalculatorLibrary
{
  public class Calculator
  {
    JsonWriter writer;
    public int TimesUsedCount { get; set; }

    public Calculator()
    {
      StreamWriter logFile = File.CreateText("calculatorlog.json");
      logFile.AutoFlush = true;
      writer = new JsonTextWriter(logFile);
      writer.Formatting = Formatting.Indented;
      writer.WriteStartObject();
      writer.WritePropertyName("Operations");
      writer.WriteStartArray();
      TimesUsedCount = 0;
    }

    // CalculatorLibrary.cs
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
      switch (op.ToUpper())
      {
        case "A":
          result = num1 + num2;
          writer.WriteValue("Add");
          break;
        case "S":
          result = num1 - num2;
          writer.WriteValue("Subtract");
          break;
        case "M":
          result = num1 * num2;
          writer.WriteValue("Multiplication");
          break;
        case "D":
          // Ask the user to enter a non-zero divisor.
          if (num2 != 0)
          {
            result = num1 / num2;
            writer.WriteValue("Divide");
          }
          break;
        // Return text for an incorrect option entry.
        default:
          break;
      }

      CalculationList.AddCalculation(num1, num2, op, result);

      writer.WritePropertyName("Result");
      writer.WriteValue(result);
      writer.WriteEndObject();

      TimesUsedCount += 1;

      return result;
    }

    public void LogList(string op)
    {
      switch (op)
      {
        case "SL":
          CalculationList.ShowList();
          break;
        case "DL":
          CalculationList.ClearList();
          break;
      }
    }

    public void Finish()
    {
      writer.WriteEndArray();
      writer.WriteEndObject();
      writer.Close();
    }
  }

}
