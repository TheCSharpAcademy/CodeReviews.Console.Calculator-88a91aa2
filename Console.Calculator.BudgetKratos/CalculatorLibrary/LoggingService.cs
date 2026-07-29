using CalculatorLibrary.Models;

namespace CalculatorLibrary;

public class LoggingService
{
    public List<Equation> UpdateHistory(Equation equationToAdd, List<Equation> history)
    {
        history.Add(new Equation
        {
            Operand1 = equationToAdd.Operand1,
            Operand2 = equationToAdd.Operand2,
            OperationPerformed = equationToAdd.OperationPerformed,
            Result = equationToAdd.Result
        });

        return history;
    }
    public List<Equation> ClearHistory(List<Equation> history)
    {
        history.Clear();

        return history;
    }
}
