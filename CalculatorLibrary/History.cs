namespace CalculatorLibrary;

using System.Text.RegularExpressions;
using Newtonsoft.Json;

public static class History
{
    public static CalculatorData data = LoadHistory();
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
            return JsonConvert.DeserializeObject<CalculatorData>(json) ?? new CalculatorData();
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

        Console.WriteLine("\t\t[Calculator History]\n");
        Console.WriteLine($"\t\tCalculator was used {data.UsageCount} times!");

        foreach (var operation in data.Operations)
        {
            opNum++;
            Console.WriteLine($"-- Operation {opNum} --");

            if (Regex.IsMatch(operation.Operation, "^(sin|cos|tan|Sqrt|10x)$", RegexOptions.IgnoreCase)){
                
                Console.WriteLine($"{operation.Operation}({operation.Operand1}) = {operation.Result}\n");

            } else
            {
                string symbol = operation.Operation switch
                {
                    "Add"         => "+",
                    "Subtract"    => "-",
                    "Multiply"    => "*",
                    "Divide"      => "/",
                    "Power"       => "^",
                    _             => "?"
                };

                
                Console.WriteLine($"{operation.Operand1} {symbol} {operation.Operand2} = {operation.Result}\n");
            }

            
        }
    }

    public static void DeleteHistory()
    {
        var emptyData = new CalculatorData
        {
            UsageCount = 0,
            Operations = new List<OperationObject>()
        };

        File.WriteAllText("Calculator.json",
            JsonConvert.SerializeObject(emptyData, Formatting.Indented));

        data = emptyData;
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