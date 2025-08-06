namespace CalculatorProgram
{
    internal class Helpers
    {
        internal void ClearConsoleColors()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
        }

        internal void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║          CALCULATOR APP v1.0         ║");
            Console.WriteLine("║     Welcome to the Calculator App    ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.WriteLine();
            Console.ResetColor();
        }

        internal void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("╔═══ ERROR ═══╗");
            Console.WriteLine($"║ {message,-11} ║");
            Console.WriteLine("╚═════════════╝");
            Console.WriteLine();
            Console.ResetColor();
        }


        internal void PrintExitMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════════════════════════╗");
            Console.WriteLine("║  Thank you for using          ║");
            Console.WriteLine("║  CALCULATOR APP!              ║");
            Console.WriteLine("║  Goodbye!                     ║");
            Console.WriteLine("╚═══════════════════════════════╝");
            Console.WriteLine();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("═══════════════════════════════════════════");
            Console.WriteLine("Press any key to exit the application...");
            Console.ResetColor();
            Console.ReadKey();
        }

        internal void PrintFooter()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("═══════════════════════════════════════════");
            Console.WriteLine("Press any key to exit the application...");
            Console.ResetColor();
        }

        internal void PrintHistory(List<string> history, int totalUsageCount)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════ HISTORY ═════════════════╗");

            if (history.Count == 0)
            {
                Console.WriteLine("║        No operations performed yet.       ║");
            }
            else
            {
                int entryNumber = 1;
                foreach (var entry in history)
                {
                    // Trim to avoid breaking the box if string too long
                    string safeEntry = entry.Length > 30 ? entry.Substring(0, 27) + "..." : entry;
                    Console.WriteLine($"║Ths app is used: {totalUsageCount} times: {safeEntry,-18} ║");
                    entryNumber++;
                }
            }

            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
