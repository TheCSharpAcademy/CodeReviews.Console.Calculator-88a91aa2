namespace CalculatorResults
{
    public class ResultPrinter
    {
        public void PrintResult(double first, double second, double result, char operation)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔══════════════════ RESULT ══════════════════╗");

            string operationSymbol = operation switch
            {
                'A' or 'a' => "+",
                'S' or 's' => "-",
                'M' or 'm' => "*",
                'D' or 'd' => "/",
                _ => "?"
            };

            string operationName = operation switch
            {
                'A' or 'a' => "Addition",
                'S' or 's' => "Subtraction",
                'M' or 'm' => "Multiplication",
                'D' or 'd' => "Division",
                _ => "Unknown"
            };

            // Helper to format numbers: show as int if whole number, else show with 2 decimals
            string FormatNumber(double num) =>
                (num % 1 == 0) ? num.ToString("0") : num.ToString("0.##");

            string formattedFirst = FormatNumber(first);
            string formattedSecond = FormatNumber(second);
            string formattedResult = FormatNumber(result);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"║                                            ║");
            Console.WriteLine($"║  Operation: {operationName,-30} ║");
            Console.WriteLine($"║  Expression: {formattedFirst} {operationSymbol} {formattedSecond} = {formattedResult,-20} ║");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"║  ANSWER: {formattedResult,-32} ║");
            Console.WriteLine($"║                                            ║");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
