using CalculatorLibrary;

namespace CalculatorProgram
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new();
            CalculatorApp calculatorApp = new(calculator);
            calculatorApp.Start();
            calculator.Finish();
        }
    }
}
  
