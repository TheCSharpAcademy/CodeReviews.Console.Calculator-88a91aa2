using CalculatorLibrary;
using CalculatorMenu;


namespace CalculatorProgram
{
    public class Program
    {
        private static Calculator calcLib = Calculator.Instance;
        private static CalculatorFunctions calculatorFunctions = new CalculatorFunctions();

        public static void Main(string[] args)
        {
            Programme();
        }

        static void Programme()
        {
            var menu = new Menu();
            bool running = true;

            while (running)
            {
                string selected = menu.ShowMainMenu();

                switch (selected)
                {
                    case "New Calculation":
                        calculatorFunctions.Function();
                        break;

                    case "View Result History":
                        calcLib.GetLastResult();
                        break;

                    case "Use Previous Result":
                        menu.ShowHistoryMenu();
                        break;

                    case "View Calculator Usage Count":
                        calcLib.GetUsageCount();
                        break;

                    case "Clear History":
                        calcLib.ClearHistory();
                        break;

                    case "Exit":
                        calcLib.Finish();
                        running = false;
                        break;
                }
            }
        }
    }
}