namespace CalculatorLibrary;


using System.Buffers;
using System.Diagnostics;
using Newtonsoft.Json;
public class Calculator
{
    
    public CalculatorData data = History.LoadHistory();
    private int usageCount;

    
    public Calculator() {

        
        
    }
    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        OperationObject OperationObj = new OperationObject();
        OperationObj.Operand1 = num1;
        OperationObj.Operand2 = num2;

        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                OperationObj.Operation = "Add";
                OperationObj.Result = result;
                break;
            case "s":
                result = num1 - num2;
                OperationObj.Operation = "Subtract";
                OperationObj.Result = result;
                break;
            case "m":
                result = num1 * num2;
                OperationObj.Operation = "Multiply";
                OperationObj.Result = result;
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                    OperationObj.Operation = "Divide";
                    OperationObj.Result = result;
                } else
                {
                    OperationObj.Operation = "Divide";
                    OperationObj.Result = double.NaN;
                }
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        
        data.Operations.Add(OperationObj);
        History.data = data;
        History.SaveHistory();
        return result;
    }

}