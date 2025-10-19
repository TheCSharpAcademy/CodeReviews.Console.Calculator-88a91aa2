using Spectre.Console;
using System;

namespace Calculator;
internal class LatestOperations
{
    private static List<(string operation, double result)> LastResults = [];
    public double? SelectedResult { get; private set; }
    public void AddOperation(string operation, double result) => LastResults.Add((operation, result));
    public void DeleteOperations() => LastResults.Clear();

    public void SelectOperation()
    {
        if(LastResults.Count == 0) 
        {
            return;
        }

        Console.WriteLine("Select the number of the operation do you want to use");

        if (!int.TryParse(Console.ReadLine(), out int index)) 
        {
            Console.WriteLine("Write a correct number");
        }

        SelectedResult = LastResults[index - 1].result;

        AnsiConsole.Markup($"Result selected: [#B8E0D2]{SelectedResult}[/]");
        Console.ReadKey();
    }

    public void ViewList()
    {
        int index = 1;

        if (LastResults.Count == 0)
        {
            AnsiConsole.MarkupLine("[#7F2CCB]Do some operations!!![/]");
        }

        foreach (var (operation, result) in LastResults)
        {
            AnsiConsole.MarkupLine($"{index}. lates result [#815355]{operation}[/]: [#7BF1A8]{result}[/]");
            index++;
        }
    }
}

