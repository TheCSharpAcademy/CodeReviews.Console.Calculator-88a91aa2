namespace CalculatorLibrary;
using Newtonsoft.Json;

public static class History
{
    public static CalculatorData data;
    public static CalculatorData LoadHistory()
    {
        // Using JSON serialization instead of streaming

        if (!File.Exists("Calculator.json"))
        {
            var emptyData = new CalculatorData
            {
                UsageCount = 0,
                Operations = new List<OperationObject>()
            };

            string json = JsonConvert.SerializeObject(emptyData, Formatting.Indented);
            File.WriteAllText("Calculator.json", json);
            return emptyData;

        } else
        {
            string json = File.ReadAllText("Calculator.json");
            data = JsonConvert.DeserializeObject<CalculatorData>(json) ?? new CalculatorData();
            return data;
        }

    }

    public static void SaveHistory()
    {
        data.UsageCount++;
        File.WriteAllText("Calculator.json", JsonConvert.SerializeObject(data, Formatting.Indented));
    }

    public static void ShowHistory()
    {
        int opNum = 0;
        var data = LoadHistory();

        Console.WriteLine("\t\t[Calculator History]\n");
        foreach (var operation in data.Operations)
        {
            opNum++;
            string symbol = operation.Operation switch
            {
                "Add"       => "+",
                "Subtract"  => "-",
                "Multiply"  => "*",
                "Divide"    => "/",
                _           => "?"
            };

            Console.WriteLine($"-- Operation {opNum} --");
            Console.WriteLine($"{operation.Operand1} {symbol} {operation.Operand2} = {operation.Result}\n");
        }
    }

    public static void DeleteHistory()
    {
        File.WriteAllText("Calculator.json", "");
        Console.WriteLine("> History Deleted Successfully!\n");
    }
}

public class OperationObject
{
    public double Operand1 { get; set; }
    public double Operand2 { get; set; }
    public string Operation { get; set; } = "";
    public double Result { get; set; } = 0;
}

public class CalculatorData
{
    public int UsageCount { get; set; } = 0;
    public List<OperationObject> Operations { get; set;} = new();
}