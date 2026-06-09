namespace CalculatorProgram;

class CalculationRequest (double cleanNumber1, double cleanNumber2, string op)
{
    public double LeftNumber { get; } = cleanNumber1;
    public double RightNumber { get; } = cleanNumber2;
    public string Op { get; } = op;

}