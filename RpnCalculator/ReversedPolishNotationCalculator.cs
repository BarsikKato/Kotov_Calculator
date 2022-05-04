using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpnCalculator
{
    internal class ReversedPolishNotationCalculator
    {
        private List<string> operators = new List<string>() { "+", "-", "*", "/", "^", "%", "s", "m", "(", ")" };
        public decimal GetResult(string input)
        {
            Stack<string> stack = new Stack<string>();
            Queue<string> queue = new Queue<string>(SeparateExpression(input));
            string str = queue.Dequeue();
            while (queue.Count >= 0)
            {
                if (!operators.Contains(str))
                {
                    stack.Push(str);
                    str = queue.Dequeue();
                }
                else
                {
                    decimal? calculationResult = OperatorCalculation(str, stack);
                    stack.Push(calculationResult.ToString());

                    if (queue.Count > 0)
                        str = queue.Dequeue();
                    else
                        break;
                }
            }
            if (stack.Count != 1)
                throw new Exception("Ошибка ввода данных.");
            return Convert.ToDecimal(stack.Pop());
        }

        private IEnumerable<string> SeparateExpression(string input)
        {
            BracketsChecker(input);
            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsDigit(input[i]) && !operators.Contains(input[i].ToString()))
                    continue;
                yield return input[i].ToString();
            }
        }

        private void BracketsChecker(string input)
        {
            int leftBrackets = input.Where(x => x == '(').Count();
            int rightBrackets = input.Where(x => x == ')').Count();

            if (leftBrackets != rightBrackets)
                throw new Exception("Ошибка ввода выражения.");
        }

        public string ConvertToReversedPolishNotation(string input)
        {
            string output = string.Empty;
            Stack<string> operatorStack = new Stack<string>();
            foreach (string c in SeparateExpression(input))
            {
                if (operators.Contains(c))
                {
                    if (operatorStack.Count > 0 && !c.Equals("("))
                    {
                        if (c.Equals(")"))
                        {
                            string s = operatorStack.Pop();
                            while (s != "(")
                            {
                                output += s;
                                s = operatorStack.Pop();
                            }
                        }
                        else if (GetOperatorPriority(c) > GetOperatorPriority(operatorStack.Peek()))
                        {
                            operatorStack.Push(c);
                        }
                        else
                        {
                            while (operatorStack.Count > 0 && GetOperatorPriority(c) <= GetOperatorPriority(operatorStack.Peek()))
                            {
                                output += operatorStack.Pop();
                            }
                            operatorStack.Push(c);
                        }
                    }
                    else
                    {
                        operatorStack.Push(c);
                    }
                }
                else
                {
                    output += c;
                }
            }
            if (operatorStack.Count > 0)
                foreach (string c in operatorStack)
                    output += c;

            return output;
        }

        private byte GetOperatorPriority(string oper)
        {
            switch (oper)
            {
                case "(":
                case ")":
                    return 0;
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                case "%":
                    return 2;
                case "^":
                    return 3;
                default:
                    return 4;
            }
        }

        private decimal? OperatorCalculation(string str, Stack<string> stack)
        {

            if (str == "s")
            {
                double x = double.Parse(stack.Pop());
                return Convert.ToDecimal(Math.Sqrt(x));
            }

            decimal a = Convert.ToDecimal(stack.Pop());
            decimal b = Convert.ToDecimal(stack.Pop());
            switch (str)
            {
                case "+":
                    return a + b;
                case "-":
                    return b - a;
                case "*":
                    return a * b;
                case "/":
                    return b / a;
                case "%":
                    return b / a * 100;
                case "m":
                    return b % a;
                case "^":
                    double pow = Math.Pow(Convert.ToDouble(b), Convert.ToDouble(a));
                    return Convert.ToDecimal(pow);
                default:
                    return null;
            }
        }
    }
}
