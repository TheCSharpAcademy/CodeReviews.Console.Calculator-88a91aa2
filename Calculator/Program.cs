using CalculatorLibrary;
using System.Text.RegularExpressions;

class Program
{
  static void Main(string[] args)
  {
    bool endApp = false;
    // Display title as the C# console calculator app.
    Console.WriteLine("Console Calculator in C#\r");
    Console.WriteLine("------------------------\n");

    Calculator calculator = new Calculator();

    while (!endApp)
    {
      Console.WriteLine($"Times calculator's used: {calculator.TimesUsedCount}\n");

      // Ask the user to choose an operator.
      Console.WriteLine("Choose an operator from the following list:");
      Console.WriteLine("\tA - Add");
      Console.WriteLine("\tS - Subtract");
      Console.WriteLine("\tM - Multiply");
      Console.WriteLine("\tD - Divide");
      Console.WriteLine("\tSL - Show Calculation Logs");
      Console.WriteLine("\tDL - Delete Calculation Logs");

      Console.Write("\nYour option? ");

      string? op = Console.ReadLine()?.ToUpper();

      if (op == null || !Regex.IsMatch(op, "[A|S|M|D|SL|DL]"))
      {
        Console.WriteLine("Error: Unrecognized input.");
      }
      else if (Regex.IsMatch(op, "^(A|S|M|D)$"))
      {
        try
        {
          string? numInput1 = "";
          string? numInput2 = "";
          double result = 0;

          bool usePastResult = false;

          Console.Clear();

          Console.WriteLine("Do you want to use the last result?");
          Console.WriteLine("Y - Yes");
          Console.WriteLine("N - No");
          Console.Write("\nChoice: ");

          string? yesOrNo = "";
          while (yesOrNo == "")
          {
            yesOrNo = Console.ReadLine()?.Trim();
          }

          if (yesOrNo?.ToUpper() == "Y")
          {
            numInput1 = CalculationList.GetLastResult().ToString();
            usePastResult = true;
          }

          Console.Clear();

          double cleanNum1 = 0;
          if (usePastResult)
          {
            cleanNum1 = Convert.ToDouble(numInput1);
            Console.WriteLine($"First number: {cleanNum1}");
          }
          else
          {
            Console.Write("Type a number, and then press Enter: ");
            numInput1 = Console.ReadLine();

            while (!double.TryParse(numInput1, out cleanNum1))
            {
              Console.Write("This is not valid input. Please enter a numeric value: ");
              numInput1 = Console.ReadLine();
            }
          }

          // Ask the user to type the second number.
          Console.Write("Type another number, and then press Enter: ");
          numInput2 = Console.ReadLine();

          double cleanNum2 = 0;
          while (!double.TryParse(numInput2, out cleanNum2))
          {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput2 = Console.ReadLine();
          }

          result = calculator.DoOperation(cleanNum1, cleanNum2, op);
          if (double.IsNaN(result))
          {
            Console.WriteLine("This operation will result in a mathematical error.\n");
          }
          else Console.WriteLine("\nYour result: {0:0.##}\n", result);
        }
        catch (Exception e)
        {
          Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
        }
      }
      else
      {
        calculator.LogList(op);
      }

      Console.WriteLine("\n------------------------\n");

      // Wait for the user to respond before closing.
      Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
      if (Console.ReadLine() == "n") endApp = true;

      Console.Clear();
    }

    calculator.Finish();

    return;
  }
}
