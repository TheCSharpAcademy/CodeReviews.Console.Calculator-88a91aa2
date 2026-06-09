using CalculatorLibrary;

namespace CalculatorProgram;

class CalculatorApp
{
    private readonly Calculator _calculator;

    public CalculatorApp(Calculator calculator)
    {
        _calculator = calculator;
    }
        

    public void Start()
    {
        CalculationHistory history = new();
        InputHandler inputHandler = new();
        bool endApp = false;
        while (!endApp)
        {
                
            var calculationRequest =  ShowMainMenu(inputHandler, history);
            RunCalculation(calculationRequest.LeftNumber, calculationRequest.RightNumber, calculationRequest.Op, history);
            Console.WriteLine("------------------------\n");
            Console.Write("Press 'n' to exit, 'h' to show the calculation history or press Enter to continue: ");
                
                
            var input = Console.ReadLine();
            Console.Clear();

            if (HandleUserChoice(input, endApp, history))
            {
                endApp = true;
            }
                
            Console.WriteLine("\n");
        }
    }
        

    private bool HandleUserChoice(string? input, bool endApp, CalculationHistory history)
    {
        switch (input)
        {
            case "n":
                endApp = true;
                break;
            case "h":
            {
                history.Print();
                Console.WriteLine(history.Count() == 0
                    ? "Press Enter to continue"
                    : "Press 'd' then Enter to delete the history or Enter to continue");
                var answer = Console.ReadLine();
                Console.Clear();
                if (answer == "d")
                {
                    history.Delete();
                }

                break;
            }
        }

        return endApp;
    }

    private CalculationRequest ShowMainMenu(InputHandler inputHandler, CalculationHistory history)
    {
        double cleanNum1 = inputHandler.GetValidNumber(history);
        double cleanNum2 = inputHandler.GetValidNumber(history);
        string op = inputHandler.GetValidOperation();
        CalculationRequest calculationRequest = new(cleanNum1, cleanNum2, op);
        return calculationRequest;
    }
        
    private void RunCalculation(double cleanNum1, double cleanNum2, string op, CalculationHistory history)
    {
        try
        {
            double result = _calculator.DoOperation(cleanNum1, cleanNum2, op);
            if (double.IsNaN(result))
            {
                Console.WriteLine("This operation will result in a mathematical error.\n");
            }
            else
            {
                Console.WriteLine("Your result: {0:0.##}\n", result);
                Calculation calculation = new(cleanNum1, cleanNum2, op, result);
                history.Add(calculation);
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
        }
            
    }
}