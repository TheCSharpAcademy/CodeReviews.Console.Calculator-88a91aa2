namespace CalculatorProgram;

class CalculationHistory
{
    private readonly List<Calculation> History = [];

    public void Print()
    {
        if (History.Count == 0)
        {
            Console.WriteLine("There is no history to show");
            Console.WriteLine("Press Enter to continue");
        }
        else
        {
            Console.WriteLine("HISTORY");
            Console.WriteLine("-----------------");
            foreach (var (index, calculation) in History.Index())
            {
                char operation = calculation.Operation switch
                {
                    "a" => '+',
                    "s" => '-',
                    "m" => '*',
                    "d" => '/',
                    _ => throw new InvalidOperationException()
                };
                Console.WriteLine(
                    $"{index + 1}) {calculation.LeftNumber} {operation} {calculation.RightNumber} = {calculation.Result}");
            }

            Console.WriteLine("-----------------");
            Console.WriteLine("The calculator has been used " + History.Count + " " +
                              (History.Count == 1 ? "time" : "times"));
        }

    }

    public void Add(Calculation calculation)
    {
        History.Add(calculation);
    }

    public void Delete()
    {
        History.Clear();
        Console.WriteLine("History deleted");
    }

    public int Count()
    {
        return History.Count;
    }

    public double GetResult(int index)
    {
        return History[index].Result;
    }
        
}