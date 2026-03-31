using System.ComponentModel.Design;
using System.Formats.Asn1;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Xml;
using CalculatorLibrary.cs;

namespace CalculatorConsole.Ledana
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            using Calculator calculator = new();
            CalculatorUI ui = new();
            int count = 0;

            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            while (true)
            {
                try
                {
                    if (!ui.ShouldContinue()) break;

                    if (calculator.Calculations.Count > 0 && ui.HandleHistory(calculator, ref count))
                        continue;

                    double cleanNum1 = ui.GetNumber("Type a number, and then press Enter");
                    string? op = ui.GetOperation();

                    if (ui.HandleSingleOrTrig(calculator, cleanNum1, op, ref count))
                        continue;

                    double cleanNum2 = ui.GetNumber("Type another number, and then press Enter");


                    double result = calculator.DoOperation(cleanNum1, op, cleanNum2);
                    ui.PrintResult(result, ref count);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Oh no! An exception occurred: {e.Message}");
                }
            }
        }
    }
}
