namespace CalculatorProgram;

class Calculation(double leftNumber, double rightNumber, string @operation, double result)
{
    public readonly double LeftNumber = leftNumber;
    public readonly double RightNumber = rightNumber;
    public readonly string Operation = @operation;
    public readonly double Result = result;
}