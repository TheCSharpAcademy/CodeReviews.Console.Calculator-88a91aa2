namespace CalculatorLibrary
{
  public class Calculation
  {
    public double Num1;
    public double Num2;
    public string Operation;
    public double Result;

    internal Calculation(double num1, double num2, string operation, double result)
    {
      Num1 = num1;
      Num2 = num2;
      Operation = operation;
      Result = result;
    }
  }
}
