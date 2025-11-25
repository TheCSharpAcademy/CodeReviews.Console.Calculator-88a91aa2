namespace CalculatorLibrary;

public record Operation(
    OperationType Type,
    double Value1,
    double Value2,
    double Result
)
{
    public override string ToString()
    {
        var symbol = Type switch
        {
            OperationType.Add => "+",
            OperationType.Subtract => "-",
            OperationType.Multiply => "*",
            OperationType.Divide => "/",
            OperationType.SquareRoot => "√",
            OperationType.Power => "^",
            OperationType.TenX => "^",
            _ => throw new ArgumentOutOfRangeException()
        };

        return $"{Value1} {symbol} {Value2} = {Result}";
    }
}