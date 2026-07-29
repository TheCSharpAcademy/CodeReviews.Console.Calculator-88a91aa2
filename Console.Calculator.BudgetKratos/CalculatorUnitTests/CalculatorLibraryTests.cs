using CalculatorLibrary;

namespace CalculatorUnitTests;

public class CalculatorLibraryTests
{
    private static readonly object[] ValidOperandTestCases =
    {
        new TestCaseData("1.23", 1.23d),
        new TestCaseData("123", 123d),
        new TestCaseData("-10234", -10234d),
        new TestCaseData("-1234.567", -1234.567),
        new TestCaseData("-1234.567", -1234.567)
    };

    private static readonly object[] InvalidOperandTestCases =
    {
        new TestCaseData("Visca Barça i Visca Catalunya", "86.23", 86.23d),
        new TestCaseData("hahawhat", "789", 789d),
        new TestCaseData("\t\n\r", "-98743", -98743d),
        new TestCaseData("@#$", "-43467.646", -43467.646d),
        new TestCaseData("@#$", "-43467.646", -43467.646d)
    };

    private static readonly object[] ValidSimpleOperationTestCases =
    {
        new TestCaseData((1234d, 7777d), "m", (1234d*7777d)),
        new TestCaseData((-98d, 887d), "m", (-98d*887d)),
        new TestCaseData((4.6d, -87.6d), "m", (4.6d*-87.6d)),
        new TestCaseData((-0.44d,-1.78d), "m", (-0.44d*-1.78d)),
        new TestCaseData((8.623d, 1512d), "m", (8.623d*1512d)),

        new TestCaseData((0.04d, 9.96), "a", (0.04d+9.96)),
        new TestCaseData((-98d, -7d), "a", (-98d+-7d)),
        new TestCaseData((-64.6d, 8.6d), "a", (-64.6d+8.6d)),
        new TestCaseData((-1.64d,0.88d), "a", (-1.64d+0.88d)),
        new TestCaseData((151225d, 8623d), "a", (151225d+8623d)),

        new TestCaseData((0.04d, 9.96), "s", (0.04d-9.96)),
        new TestCaseData((-9d, -7d), "s", (-9d-(-7d))),
        new TestCaseData((-64.6d, 8.6d), "s", (-64.6d-8.6d)),
        new TestCaseData((-1.64d,0.88d), "s", (-1.64d-0.88d)),
        new TestCaseData((1512d, 8623d), "s", (1512d-8623d)),

        new TestCaseData((0.04d, 0d), "d", double.NaN),
        new TestCaseData((0d, -7d), "d", 0),
        new TestCaseData((-64.6d, 8.6d), "d", (-64.6d/8.6d)),
        new TestCaseData((-1.64d,0.8d), "d", (-1.64d/0.8d)),
        new TestCaseData((8623d, -8.623), "d", (8623d/-8.623))
    };

    private static readonly object[] InvalidSimpleOperationTestCases =
    {
        new TestCaseData((476d, 8594d), "f"),
        new TestCaseData((-0.6d, -1919d), " "),
        new TestCaseData((-5555d, -12345567d), "$"),
        new TestCaseData((3829d, 3268d), "\n\r\t"),
        new TestCaseData((4738d, 192941d), "%d")
    };

    private static readonly object[] ValidComplexOperationTestCases =
    {
        new TestCaseData(12.5d, null, 156.25d, "Square"),
        new TestCaseData(144d, null, 12d, "SquareRoot"),
        new TestCaseData(8.623d, null, 86.23d, "TenX"),
        new TestCaseData(3.14d, 3, 30.959d, "PowerOfX"),
        new TestCaseData(3.14d, 5, 305.245d, "PowerOfX"),
        new TestCaseData(8.6d, null, 0.73d, "Sine"),
        new TestCaseData(2.3d, null, -0.66d, "Cosine"),
        new TestCaseData(1.225d, null, 2.77d, "Tangent")
    };

    private Calculator _calculator;

    [SetUp]
    public void Setup()
    {
        _calculator = new();
    }

    [TestCaseSource(nameof(ValidOperandTestCases))]
    public void ValidInput_ReturnsOperand(string input, double expectedDoubleOperand)
    {
        double result = _calculator.ExtractOperandFromUserInput(input);

        Assert.That(result, Is.EqualTo(expectedDoubleOperand));
    }

    [TestCaseSource(nameof(InvalidOperandTestCases))]
    public void InvalidInputIsRejected_UserIsPromptedAgain(string invalidInput, string validInput, double expectedDoubleOperand)
    {
        int callCount = 0;

        Func<string?> mockInput = () =>
        {
            callCount++;
            return callCount == 1 ? invalidInput : validInput;
        };

        Calculator _calculatorWithInput = new Calculator(mockInput);

        double result = _calculatorWithInput.ExtractOperandFromUserInput("invalidInput");

        Assert.That(result, Is.EqualTo(expectedDoubleOperand));
        Assert.That(callCount, Is.EqualTo(2));  
    }

    [TestCaseSource(nameof(ValidSimpleOperationTestCases))]
    public void ValidOperations_ReturnValidResults((double, double) operands, string? operation, double expectedResult)
    {
        double result = _calculator.PerformOperation(operands.Item1, operands.Item2, operation);

        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [TestCaseSource(nameof(InvalidSimpleOperationTestCases))]
    public void InvalidOperations_ReturnNotANumber((double, double) operands, string operation)
    {
        double result = _calculator.PerformOperation(operands.Item1, operands.Item2, operation);

        Assert.That(result, Is.NaN);
    }

    [TestCaseSource(nameof(ValidComplexOperationTestCases))]
    public void ValidOperandToComplexOperation_GetsCorrectResult(double firstOperand, double secondOperand, double expectedResult, string operationPerformed)
    {
        double result = _calculator.PerformOperation(firstOperand, secondOperand, operationPerformed);

        Assert.That(result, Is.EqualTo(expectedResult).Within(5).Percent);
    }

}
