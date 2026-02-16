using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    public class Result
    {
        public double Operand1 { get; set; }
        public double? Operand2 { get; set; }
        public string Operation { get; set; } = "";
        public double Answer { get; set; }

        public void Display()
        {
            Console.WriteLine($"Operand1 = {Operand1} Operation: {Operation} Operand2 = {Operand2} = Result {Answer}");
        }

    }
}
