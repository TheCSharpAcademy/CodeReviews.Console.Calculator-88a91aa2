using CalculatorLibrary.Models;

namespace CalculatorLibrary;

public class Calculator(Func<string?> readLine = null)
{
    private readonly Func<string?> _readLine = readLine ?? (() => Console.ReadLine());
    private LoggingService _logger = new();
    private List<Equation> history = new();   

    public double ExtractOperandFromUserInput(string userInput)
    {
        double cleanOperand = double.NaN;

        while (!double.TryParse(userInput, out cleanOperand))
        {
            Console.WriteLine("Invalid input, please enter a numeric value: ");
            userInput = _readLine();
        }

        return cleanOperand;
    }
    public List<Equation> GetHistory() => history;
    public List<Equation> MemoryClear() => _logger.ClearHistory(history);
    public double PerformOperation(double firstOperand, double secondOperand, string operationPerformed)
    {
        Equation currentEquation = new Equation
        {
            Operand1 = firstOperand,
            Operand2 = secondOperand
        };

        (double answer, string operation) resultTuple = (double.NaN, String.Empty);

        resultTuple = operationPerformed switch
        {
            "Divide" when secondOperand != 0 => (firstOperand / secondOperand, "Division"),
            "Add" => (firstOperand + secondOperand, "Addition"),
            "Subtract" => (firstOperand - secondOperand, "Subtraction"),
            "Multiply" => (firstOperand * secondOperand, "Multiplication"),
            "Square" => (firstOperand * firstOperand, "Square of x"),
            "SquareRoot" => (Math.Sqrt(firstOperand), "Square Root of x"),
            "TenX" => (firstOperand * 10, "10x"),
            "PowerOfX" => (Math.Pow(firstOperand, secondOperand), "x to power of y"),
            "Sine" => (Math.Sin(firstOperand), "Sine of x"),
            "Cosine" => (Math.Cos(firstOperand), "Cosine of x"),
            "Tangent" => (Math.Tan(firstOperand), "Tangent of x"),
            _ => resultTuple
        };

        currentEquation.Result = resultTuple.answer;
        currentEquation.OperationPerformed = resultTuple.operation;

        history = _logger.UpdateHistory(currentEquation, history);

        return resultTuple.answer;
    }

}
