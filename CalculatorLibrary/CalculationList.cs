namespace CalculatorLibrary
{
  public static class CalculationList
  {
    static List<Calculation> calculations = new();

    public static void AddCalculation(double num1, double num2, string op, double result)
    {
      switch (op)
      {
        case "A":
          op = "+";
          break;
        case "S":
          op = "-";
          break;
        case "M":
          op = "x";
          break;
        case "D":
          op = "/";
          break;
      }

      calculations.Add(new Calculation(num1, num2, op, result));
    }

    public static void ClearList()
    {
      Console.WriteLine("Successfully deleted logs.");
      calculations.Clear();
    }

    public static void ShowList()
    {
      Console.Clear();
      if (calculations.Count > 0)
      {
        Console.WriteLine("==== Calculation History ====\n");

        // Prints descending order
        for (int i = calculations.Count - 1; i >= 0; i--)
        {
          Console.WriteLine($"Calculation: {calculations[i].Num1} {calculations[i].Operation} {calculations[i].Num2} = {calculations[i].Result}");
        }
      }
      else
      {
        Console.WriteLine("Empty logs.");
      }
    }

    public static double GetLastResult()
    {
      return calculations.Count > 0 ? calculations[^1].Result : 0;
    }
  }
}
