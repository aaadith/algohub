using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public enum Operator
    {
        ADD,
        SUBTRACT,
        MULTIPLY,
        DIVIDE,
        OPENBRACE,
        CLOSEBRACE
    }
    public class Calculator
    {

        Stack<int> operands = new Stack<int>();
        Stack<Operator> operators = new Stack<Operator>();

        Dictionary<Operator, int> Precedence = new Dictionary<Operator, int> { { Operator.ADD, 1 }, 
        { Operator.SUBTRACT, 1 }, { Operator.MULTIPLY, 2 }, { Operator.DIVIDE, 3 }, { Operator.OPENBRACE, 4 }, 
        { Operator.CLOSEBRACE, 4 } };
        public String Compute(string expression)
        {
            string result = "";


            foreach (string token in expression.Split())
            {
                int operand;
                if (Int32.TryParse(token, out operand))
                {
                    operands.Push(operand);
                }
                else
                {
                    Operator currentOperator = GetOperator(token);

                    if (currentOperator == Operator.CLOSEBRACE)
                    {
                        while (operators.Peek() != Operator.OPENBRACE)
                            Evaluate();
                        operators.Pop();
                    }

                    if (operators.Count == 0)
                        operators.Push(currentOperator);
                    else
                    {
                        Operator lastOperator = operators.Peek();

                        while (operators.Count > 0 && Precedence[operators.Peek()] > Precedence[currentOperator])
                            Evaluate();
                        operators.Push(currentOperator);
                    }
                }
            }

            while (operators.Count > 0)
            {
                Evaluate();
            }

            if (operands.Count == 1)
                result = operands.Pop().ToString();
            else
                throw new ArgumentException("invalid expression");

            return result;
        }

        private void Evaluate()
        {
            int Operand2 = operands.Pop();
            int Operand1 = operands.Pop();
            Operator currentOperator = operators.Pop();

            int result = 0;
            switch (currentOperator)
            {
                case Operator.ADD: result = Operand1 + Operand2;
                    break;
                case Operator.SUBTRACT: result = Operand1 - Operand2;
                    break;
                case Operator.MULTIPLY: result = Operand1 * Operand2;
                    break;
                case Operator.DIVIDE: result = Operand1 / Operand2;
                    break;

            }

            operands.Push(result);
        }



        Operator GetOperator(string token)
        {
            Operator currentOperator = 0;
            switch (token)
            {
                case "+": currentOperator = Operator.ADD;
                    break;
                case "*": currentOperator = Operator.MULTIPLY;
                    break;
                case "-": currentOperator = Operator.SUBTRACT;
                    break;
                case "/": currentOperator = Operator.DIVIDE;
                    break;
                case "(": currentOperator = Operator.OPENBRACE;
                    break;
                case ")": currentOperator = Operator.CLOSEBRACE;
                    break;
                default: throw new ArgumentException("bad expression : unrecognized token : " + token);
            }
            return currentOperator;
        }
    }
     
}
