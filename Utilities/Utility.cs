namespace Utilities
{
    public class Utility
    {
        public (double FirstNumber, double SecondNumber) GetTwoNumbers()
        {
            double first = GetSingleNumber("first");
            double second = GetSingleNumber("second");
            return (first, second);
        }

        public char GetOperatorChoice()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("AVAILABLE OPERATIONS:");
                Console.WriteLine("────────────────────");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("   [A] + Addition");
                Console.WriteLine("   [S] - Subtraction");
                Console.WriteLine("   [M] * Multiplication");
                Console.WriteLine("   [D] / Division");
                Console.WriteLine("   [E] X Exit Program");
                Console.WriteLine("   [H] View History");
                Console.WriteLine("   [X] Clear History");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(">> Select your choice: ");
                Console.ForegroundColor = ConsoleColor.Green;

                string? input = Console.ReadLine()?.ToUpper();
                Console.WriteLine();
                Console.ResetColor();

                if (string.IsNullOrEmpty(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Please enter a choice.");
                    Console.ResetColor();
                    Console.WriteLine();
                    continue;
                }

                char choice = input[0];

                if ("ASMDHX".Contains(choice) || choice == 'E')
                {
                    return choice;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Unrecognised input. Please choose A, S, M, or D.");
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        private double GetSingleNumber(string position)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($">> Type a {position} number, and then press Enter: ");
            Console.ResetColor();

            string? input = Console.ReadLine();
            double cleanNumber;

            while (!double.TryParse(input, out cleanNumber))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"This is not valid input. Please enter a numeric value: ");
                Console.WriteLine();
                Console.ResetColor();
                input = Console.ReadLine();
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"   [✓] You entered: {cleanNumber}");
            Console.WriteLine();
            Console.ResetColor();
            return cleanNumber;
        }


    }
}
