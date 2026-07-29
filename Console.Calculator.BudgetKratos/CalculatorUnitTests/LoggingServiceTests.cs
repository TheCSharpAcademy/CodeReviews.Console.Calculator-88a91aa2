using CalculatorLibrary;
using CalculatorLibrary.Models;

namespace CalculatorUnitTests;

internal class LoggingServiceTests
{

    private LoggingService _logger = new();
    private static IEnumerable<object[]> HistoryTestCases()
    {
        List<Equation> testCases =
        [
         new Equation {Operand1 = 4.9d, Operand2 = 1d, OperationPerformed = "Add", Result = 5.9d},
         new Equation {Operand1 = -1.87d, Operand2 = 7.918d, OperationPerformed = "Multiply", Result = -18.55d},
         new Equation {Operand1 = -12.99d, Operand2 = -6.8d, OperationPerformed = "Divide", Result = 1.91d},
         new Equation {Operand1 = -155.122d, Operand2 = -200.255d, OperationPerformed = "Subtract", Result = 45.133d}
        ];

        yield return [testCases];

    }

    private static List<Equation> localHistory =
        [
         new Equation {Operand1 = 1.2d, Operand2 = 2.3d, OperationPerformed = "Add", Result = 3.5d},
         new Equation {Operand1 = -0.5d, Operand2 = 12.34d, OperationPerformed = "Multiply", Result = -6.17d},
         new Equation {Operand1 = -8.6d, Operand2 = -2.3d, OperationPerformed = "Divide", Result = 3.74d},
         new Equation {Operand1 = -15.12d, Operand2 = -20.25d, OperationPerformed = "Subtract", Result = 5.13d}
        ];

    private static readonly object[] ValidHistory =
    {
        new TestCaseData(new Equation { Operand1 = 1.2d, Operand2 = 2.3d, OperationPerformed = "Add", Result = 3.5d }, localHistory),
        new TestCaseData(new Equation { Operand1 = -6.5d, Operand2 = double.NaN, OperationPerformed = "Multiply", Result = -6.5d}, localHistory)
    };

    [TestCaseSource(nameof(ValidHistory))]
    public void ValidInput_CorrectlyUpdatesHistory(Equation equationToAdd, List<Equation> history)
    {
        _logger.UpdateHistory(equationToAdd, history);

        Assert.That(history, Has.Member(equationToAdd).UsingPropertiesComparer());
    }

    [TestCaseSource(nameof(HistoryTestCases))]
    public void GivenHistory_ClearDeletesIt(List<Equation> history)
    {
        _logger.ClearHistory(history);
        Assert.That(history, Is.Empty);
    }

}
