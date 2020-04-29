using System;
using System.Collections.Generic;
using System.Globalization;

namespace LineCalculator
{
    public class Calculator
    {
        public Dictionary<char, int> OperatorsPriority = new Dictionary<char, int>
        {
            {'+', 1},
            {'-', 1},
            {'*', 2},
            {'/', 2}
        };

        public Stack<float> Numbers = new Stack<float>();
        public Stack<char> Operators = new Stack<char>();

        public float Calculate(string line)
        {
            ParseLine(line);
            //Console.WriteLine(Numbers.Count);
            return Numbers.Pop();
        }
        
        public void ParseLine(string line)
        {
            string num = String.Empty;
            line = line.Replace(" ", "");
            //Console.WriteLine(line);

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
                        PositionOperator(c);
                    }
                    
                }
            }

            if (num != String.Empty)
            {
                Numbers.Push(ParseNumber(num));
                num = string.Empty;
            }

            int operatorsLeft = Operators.Count;
            for (int i = 0; i < operatorsLeft; i++)
            {
                Numbers.Push(CalculateTwoLastNumbers(Operators.Pop()));
            }
            
            //Console.WriteLine(Operators.Count);
        }

        public float ParseNumber(string number)
        {
            try
            {
                return float.Parse(number, CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool IsCharOperator(char c)
        {
            if (c == '+' || c == '-' || c == '*' || c == '/')
                return true;
            return false;
        }

        public void PositionOperator(char op)
        {
            if (Operators.Count < 1)
            {
                Operators.Push(op);
                return;
            }
                
            char lastOperator = Operators.Peek();
            
            int lastOperatorPriority = OperatorsPriority[lastOperator];
            int thisOperatorPriority = OperatorsPriority[op];

            if (thisOperatorPriority > lastOperatorPriority)
            {
                Operators.Push(op);
            }
            else
            {
                lastOperator = Operators.Pop();
                Numbers.Push(CalculateTwoLastNumbers(lastOperator));
                Operators.Push(op);
            }
        }

        public float CalculateTwoLastNumbers(char op)
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