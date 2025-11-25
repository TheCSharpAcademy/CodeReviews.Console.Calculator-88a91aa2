namespace CalculatorLibrary
{
    public class Calculator
    {
        private readonly Logger _logger = new();

        private Operation Execute(OperationType type, double value1, double value2, Func<double> calculate)
        {
            var result = calculate();
            var operation = new Operation(type, value1, value2, result);

            _logger.WriteOperation(operation);

            return operation;
        }

        public Operation Add(double a, double b)
            => Execute(OperationType.Add, a, b, () => a + b);

        public Operation Subtract(double a, double b)
            => Execute(OperationType.Subtract, a, b, () => a - b);

        public Operation Multiply(double a, double b)
            => Execute(OperationType.Multiply, a, b, () => a * b);

        public Operation Divide(double a, double b)
            => Execute(OperationType.Divide, a, b, () => b == 0 ? double.NaN : a / b);

        public Operation SquareRoot(double a)
            => Execute(OperationType.SquareRoot, a, 0, () => Math.Sqrt(a));

        public Operation Power(double a, double b)
            => Execute(OperationType.Power, a, b, () => Math.Pow(a, b));

        public Operation TenX(double a)
            => Execute(OperationType.TenX, a, 10, () => Math.Pow(10, a));

        public void Finish()
        {
            _logger.Finish();
        }
    }
}