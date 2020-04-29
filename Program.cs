using System;
using System.Collections.Generic;

namespace LineCalculator
{
    class Program
    {

        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            // string line = "25.6 * 5 + 4 / 5-3";
            string line = Console.ReadLine();
            float sum = calculator.Calculate(line);
            Console.WriteLine(sum);
        }
    }
}