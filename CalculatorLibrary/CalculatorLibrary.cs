namespace CalculatorLibrary;


using System.Buffers;
using System.Diagnostics;
using Newtonsoft.Json;
public class Calculator
{
    
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
                    OperationObj.Result = result;
                }
                break;

            case "p":
                result = Math.Pow(num1, num2);
                OperationObj.Operation = "Power";
                OperationObj.Result = result;
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        
        if (!string.IsNullOrEmpty(OperationObj.Operation))
        {
            History.data.Operations.Add(OperationObj);
            History.SaveHistory();
        }
        return result;
    }

    public double DoOperation(double num1, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        OperationObject OperationObj = new OperationObject();
        OperationObj.Operand1 = num1;
        
        switch (op)
        {
            case "q":
                result =  Math.Sqrt(num1);
                OperationObj.Operation = "Sqrt";
                OperationObj.Result = result;
                break;
            case "10x":
                result = Math.Pow(10, num1);
                OperationObj.Operation = "10x";
                OperationObj.Result = result;
                break;
            case "sin":
                result = Math.Sin(num1 * Math.PI / 180);
                OperationObj.Operation = "sin";
                OperationObj.Result = result;
                break;
            case "cos":
                result = Math.Cos(num1 * Math.PI / 180);
                OperationObj.Operation = "cos";
                OperationObj.Result = result;
                break;
            case "tan":
                result = Math.Tan(num1 * Math.PI / 180);
                OperationObj.Operation = "tan";
                OperationObj.Result = result;
                break;
            default:
                break; 
        }

        if (!string.IsNullOrEmpty(OperationObj.Operation))
        {
            OperationObj.Operand2 = Double.NaN;
            History.data.Operations.Add(OperationObj);
            History.SaveHistory();
        }
        return result;
    }

}