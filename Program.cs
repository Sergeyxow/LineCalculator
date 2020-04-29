using System;
using System.Collections.Generic;

namespace LineCalculator
{
    class Program
    {
        private static Dictionary<char, int> OperatorsPriority = new Dictionary<char, int>
        {
            {'+', 1},
            {'-', 1},
            {'*', 2},
            {'/', 2}
        };

        private static Stack<float> Numbers = new Stack<float>();
        private static Stack<char> Operators = new Stack<char>();
        
        static void Main(string[] args)
        {
            ParseLine("25.6 * 5 + 4 / (5-3)");

        }

        private static void ParseLine(string line)
        {
            string num = String.Empty;
            line = line.Replace(" ", "");
            Console.WriteLine(line);

            foreach (char c in line)
            {
                if (Char.IsDigit(c) || c == '.')
                {
                    num += c;
                }
                else
                {
                    if (num != String.Empty)
                    {
                        Numbers.Push(ParseNumber(num));
                        num = string.Empty;
                    }

                    if (IsCharOperator(c))
                    {
                        
                    }
                    
                }
            }
        }

        private static float ParseNumber(string number)
        {
            try
            {
                return float.Parse(number);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static bool IsCharOperator(char c)
        {
            if (c == '+' || c == '-' || c == '*' || c == '/')
                return true;
            return false;
        }

        private static void PositionOperator(char op)
        {
            char lastOperator = Operators.Peek();
            
            int lastOpertaorPriority = OperatorsPriority[lastOperator];
            int thisOperatorPriority = OperatorsPriority[op];

            if (thisOperatorPriority > lastOpertaorPriority || Operators.Count < 1)
            {
                Operators.Push(op);
            }
            else
            {
                Numbers.Push(CalculateTwoLastNumbers(lastOperator));
            }
        }

        private static float CalculateTwoLastNumbers(char op)
        {
            float last = Numbers.Pop();
            float preLast = Numbers.Pop();

            float result = 0;
            
            switch (op)
            {
                case '+':
                    result = preLast + last;
                    break;
                case '-':
                    result = preLast - last;
                    break;
                case '*':
                    result = preLast * last;
                    break;
                case '/':
                    result = preLast / last;
                    break;
            }

            return result;
        }
    }
}