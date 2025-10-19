using Spectre.Console;
using System;

namespace Calculator
{
    internal class ExpAndSqrOperations : Operations
    {
        private double FirstNumber { get; set; } = 0;
        private double SecondNumber { get; set; } = 0;

        private readonly LatestOperations LastOperation;
        public ExpAndSqrOperations(LatestOperations latestOperations)
        {
            LastOperation = latestOperations;
        }

        public void Operation(char operation)
        {
            switch (operation)
            {
                case 'e':
                    OperationName = "Exponential";

                    if (LastOperation.SelectedResult != null && UseLastOrSelectedResult())
                    {
                        FirstNumber = (double)LastOperation.SelectedResult;
                    }
                    else
                    {
                        Console.WriteLine("Write the base");
                        FirstNumber = ValidateInput(false);
                    }

                    Console.WriteLine("Write the exponent");
                    SecondNumber = ValidateInput(false);

                    Result = Math.Pow(FirstNumber, SecondNumber);
                    break;
                case 's':
                    OperationName = "Square root";

                    if (LastOperation.SelectedResult != null && UseLastOrSelectedResult())
                    {
                        FirstNumber = (double)LastOperation.SelectedResult;
                    }
                    else
                    {
                        Console.WriteLine("Write a number to find its square root");
                        FirstNumber = ValidateInput(false);
                    }

                    Result = Math.Sqrt(FirstNumber);
                    break;
                case 'x':
                    OperationName = "10x";

                    if (LastOperation.SelectedResult != null && UseLastOrSelectedResult())
                    {
                        FirstNumber = (double)LastOperation.SelectedResult;
                    }
                    else
                    {
                        Console.WriteLine("Write a number to make a 10x operation");
                        FirstNumber = ValidateInput(false);
                    }

                    Result = Math.Pow(10, SecondNumber);
                    break;
            }

            NumberOperationMade++;
            AnsiConsole.MarkupLine($"The result of [#7BF1A8]{OperationName}[/] is: [#9EE493]{Result}[/]");
            LastOperation.AddOperation(OperationName, Result);
            Console.ReadKey();
        }
    }
}
