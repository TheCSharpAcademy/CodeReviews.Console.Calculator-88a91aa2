using Spectre.Console;
using System;

namespace Calculator
{
    internal class TrigonometricOperations : Operations
    {
        private double Angle { get; set; } = 0;

        private double Radians { get; set; } = 0;

        private readonly LatestOperations LastOperation;
        public TrigonometricOperations(LatestOperations latestOperations)
        {
            LastOperation = latestOperations;
        }

        public void Operation(string operation)
        {
            OperationName = operation;

            if (LastOperation.SelectedResult != null && UseLastOrSelectedResult())
            {
                AnsiConsole.MarkupLine($"The selected result will be used as the angle value[#729B79]: {LastOperation.SelectedResult}[/]");
                Angle = (double)LastOperation.SelectedResult;
            }
            else
            {
                AnsiConsole.MarkupLine($"Write the angle to find its [#729B79]{operation}[/]");

                Angle = ValidateInput(false);
            }

            Radians = Angle * (Math.PI / 180);

            switch (operation)
            {
                case "sine":
                    Result = Math.Sin(Radians);
                    break;
                case "cosine":
                    Result = Math.Cos(Radians);
                    break;
                case "tangent":
                    Result = Math.Tan(Radians);
                    break;
            }

            NumberOperationMade++;
            AnsiConsole.MarkupLine($"The {operation} of the angle [#7BF1A8]{Angle}[/] is equal to: [#9EE493]{Result}[/]");
            LastOperation.AddOperation(OperationName, Result);
            Console.ReadKey();
        }
    }
}
