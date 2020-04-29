using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LineCalculator
{
    public class Calculator
    {
        public Dictionary<char, int> OperatorsPriority = new Dictionary<char, int>
        {
            {'+', 1},
            {'-', 1},
            {'*', 2},
            {'/', 2},
            {'(', 0},
            {')', 3}
        };

        public Stack<float> Numbers = new Stack<float>();
        public Stack<char> Operators = new Stack<char>();
        
        
        private float resultVal = 0f;
        private bool resultOk = true;
        private string errorMessage = String.Empty;

        public CalculationResult Calculate(string line)
        {
            ParseLine(line);
            //Console.WriteLine(Numbers.Count);

            if (resultOk) 
                resultVal = Numbers.Pop();
            else
            {
                resultVal = 0;
            }

            CalculationResult result = new CalculationResult
            {
                value = resultVal,
                isOk = resultOk,
                errorMessage = errorMessage
            };

            return result;
        }
        
        public void ParseLine(string line)
        {
            if (!AreBracketsEven(line))
            {
                resultOk = false;
                errorMessage = "Brackets are not even";
                return;
            }
            
            string num = String.Empty;
            line = line.Replace(" ", "");
            //Console.WriteLine(line);

            foreach (char c in line)
            {
                if (Char.IsDigit(c) || c == '.')
                {
                    num += c;
                    continue;
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
                        continue;
                    }
                    
                    //Brackets
                    
                    if (c == '(')
                    {
                        Operators.Push(c);
                        continue;
                    }
                    
                    if (c == ')')
                    {
                        while (Operators.Peek() != '(')
                        {
                            Numbers.Push(CalculateTwoLastNumbers(Operators.Pop()));
                        }

                        Operators.Pop();
                        continue;
                    }

                    resultOk = false;
                    errorMessage = "You passed the wrong formatted line";
                    break;

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

        private bool AreBracketsEven(string line)
        {
            int openBracketsCount = line.Count(c => c == '(');
            int closedBracketsCount = line.Count(c => c == ')');

            return openBracketsCount == closedBracketsCount;
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
            float preLast;

            if (Numbers.Count > 0)
                preLast = Numbers.Pop();
            else
                preLast = 0f;

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