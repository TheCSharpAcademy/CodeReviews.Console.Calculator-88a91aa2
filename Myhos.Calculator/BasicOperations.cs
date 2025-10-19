using Spectre.Console;
using System;

namespace Calculator
{
    internal class BasicOperations : Operations
    {
        private double FirstNumber { get; set; } = 0;
        private double SecondNumber { get; set; } = 0;
        private bool IsDivision { get; set; } = false;

        private readonly LatestOperations LastOperation;

        public BasicOperations(LatestOperations latestOperations)
        {
            LastOperation = latestOperations;
        }

        public void Operation(char operation)
        {
            if (operation == '/')
            {
                IsDivision = true;
            }

            if (LastOperation.SelectedResult != null && UseLastOrSelectedResult())
            {
                FirstNumber = (double) LastOperation.SelectedResult;
            }
            else
            {
                Console.WriteLine("Write first number");

                FirstNumber = ValidateInput(false);
            }

            Console.WriteLine("Write second number");

            SecondNumber = ValidateInput(IsDivision);

            switch (operation)
            {
                case '+':
                    OperationName = "Addition";
                    Result = FirstNumber + SecondNumber;
                    break;
                case '-':
                    OperationName = "Substraction";
                    Result = FirstNumber - SecondNumber;
                    break;
                case '*':
                    OperationName = "Multiplication";
                    Result = FirstNumber * SecondNumber;
                    break;
                case '/':
                    OperationName = "Division";
                    Result = FirstNumber / SecondNumber;
                    break;
            }

            NumberOperationMade++;
            AnsiConsole.MarkupLine($"{FirstNumber} {operation} {SecondNumber}: [#9EE493]{Result}[/]");
            LastOperation.AddOperation(OperationName, Result);
            Console.ReadKey();
        }
    }
}
