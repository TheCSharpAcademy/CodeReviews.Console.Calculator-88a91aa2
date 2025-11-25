using Newtonsoft.Json;

namespace CalculatorLibrary;

internal class Logger
{
    private readonly JsonWriter _writer;

    public Logger()
    {
        var logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        _writer = new JsonTextWriter(logFile);
        _writer.Formatting = Formatting.Indented;
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operations");
        _writer.WriteStartArray();
    }

    internal void WriteOperation(Operation operation)
    {
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operand1");
        _writer.WriteValue(operation.Value1);
        _writer.WritePropertyName("Operand2");
        _writer.WriteValue(operation.Value2);
        _writer.WritePropertyName("Operation");
        _writer.WriteValue(operation.Type);
        _writer.WritePropertyName("Result");
        _writer.WriteValue(operation.Result);
        _writer.WriteEndObject();
    }

    internal void Finish()
    {
        _writer.WriteEndArray();
        _writer.WriteEndObject();
        _writer.Close();
    }
}