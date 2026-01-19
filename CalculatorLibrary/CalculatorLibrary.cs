using Newtonsoft.Json;
using static CalculatorLibrary.Calculator;

namespace CalculatorLibrary;

public class CalculationRecord
{
    public double? Operand1 { get; set; }
    public double? Operand2 { get; set; }
    public OperationType Operation { get; set; }
    public double Result { get; set; }
}

public class Calculator
{
    private readonly List<CalculationRecord> _history = new();

    public enum OperationType
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        SquareRoot,
        Power,
        TenX,
        Sin,
        Cos,
        Tan
    }

    private readonly Dictionary<OperationType, int> _operandCount = new()
    {
        { OperationType.Add, 2 },
        { OperationType.Subtract, 2 },
        { OperationType.Multiply, 2 },
        { OperationType.Divide, 2 },
        { OperationType.Power, 2 },

        { OperationType.SquareRoot, 1 },
        { OperationType.TenX, 1 },
        { OperationType.Sin, 1 },
        { OperationType.Cos, 1 },
        { OperationType.Tan, 1 }
    };

    private readonly Dictionary<OperationType, string> _operationLabels = new()
    {
        { OperationType.Add, "Add (a)" },
        { OperationType.Subtract, "Subtract (s)" },
        { OperationType.Multiply, "Multiply (m)" },
        { OperationType.Divide, "Divide (d)" },
        { OperationType.Power, "Power (pow)" },
        { OperationType.SquareRoot, "Square Root (sqrt)" },
        { OperationType.TenX, "Multiply by 10 (10x)" },
        { OperationType.Sin, "Sine (sin)" },
        { OperationType.Cos, "Cosine (cos)" },
        { OperationType.Tan, "Tangent (tan)" }
    };

    public IReadOnlyDictionary<OperationType, string> OperationLabels => _operationLabels;

    private readonly Dictionary<string, OperationType> _operationMap =
        new(StringComparer.OrdinalIgnoreCase)
    {
        { "a",    OperationType.Add },
        { "s",    OperationType.Subtract },
        { "m",    OperationType.Multiply },
        { "d",    OperationType.Divide },
        { "pow",  OperationType.Power },
        { "sqrt", OperationType.SquareRoot },
        { "10x",  OperationType.TenX },
        { "sin",  OperationType.Sin },
        { "cos",  OperationType.Cos },
        { "tan",  OperationType.Tan }
    };

    public OperationType MapInputToOperation(string input)
    {

        if (_operationMap.TryGetValue(input.Trim(), out var op))
            return op;

        throw new InvalidOperationException("Unknown operation");
    }

    JsonWriter writer;

    private readonly Dictionary<OperationType, Func<double?, double?, double>> _operations;
    
    private int _usageCount = 0;
    public int UsageCount => _usageCount;

    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();

        _operations = new Dictionary<OperationType, Func<double?, double?, double>>();
        _operations[OperationType.Add] = (a, b) => a.Value + b.Value;
        _operations[OperationType.Subtract] = (a, b) => a.Value - b.Value;
        _operations[OperationType.Multiply] = (a, b) => a.Value * b.Value;

        _operations[OperationType.Divide] = (a,b) => 
        {
            if (b == 0) 
                return double.NaN;                   
            return a.Value / b.Value; 
        };

        _operations[OperationType.Power] = (a, b) => Math.Pow(a.Value, b.Value);

        _operations[OperationType.SquareRoot] = (a, b) =>
        {
            if (a < 0)
                return double.NaN;
            return Math.Sqrt(a.Value);
        };

        _operations[OperationType.Cos] = (a, b) => Math.Cos(a.Value);
        _operations[OperationType.Sin] = (a, b) => Math.Sin(a.Value);
        _operations[OperationType.Tan] = (a, b) => Math.Tan(a.Value);
        _operations[OperationType.TenX] = (a, b) => a.Value * 10;
    }

    public double DoOperation(OperationType operation, double num1, double? num2)
    {
        _usageCount++;

        double result = double.NaN; 
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");
        writer.WriteValue(operation.ToString());

        result = _operations[operation](num1, num2);

        AddToHistory(new CalculationRecord 
        { 
            Operand1 = num1,
            Operand2 = num2, 
            Operation = operation, 
            Result = result 
        });

        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        return result;
    }

    public int GetOperandCount(OperationType operation)
    {
        return _operandCount[operation];
    }

    private void AddToHistory(CalculationRecord record)
    {
        _history.Add(record);
    }
    public IReadOnlyList<CalculationRecord> GetHistory()
    {
        return _history.AsReadOnly();
    }

    public void ClearHistory()
    {
        _history.Clear();
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}
