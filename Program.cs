using System;
using System.Collections.Generic;

namespace LineCalculator
{
    class Program
    {

        static void Main(string[] args)
        {
            // string line = "25.6 * 5 + 4 / 5-3";

            while (true)
            {
                Console.WriteLine("Please enter a line to calculate");
                Calculator calculator = new Calculator();
            
                string line = Console.ReadLine();
                
                CalculationResult result = calculator.Calculate(line);

                if (result.isOk)
                {
                    Console.WriteLine("Answer is " + result.value);
                }
                else
                {
                    Console.WriteLine(result.errorMessage);
                }
            }
            
        }
    }
}