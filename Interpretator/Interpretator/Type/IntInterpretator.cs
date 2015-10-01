using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Interpretator.Type;
using Interpretator.Type.Int;

namespace Interpretator
{
    public class IntInterpretator : IInterpretatorType<int>
    {
        private static readonly string PATTERN_NUMBER = @"^\d+";


        private Context context;

        private Stack<Exp<int>> stack; 


        public IntInterpretator(Context context)
        {
            this.context = context;
        }

        public int Run(string expression)
        {
            stack = new Stack<Exp<int>>();

            expression = expression.Trim();

            E(ref expression);

            return stack.Pop().Evaluate(context);
        }

        private void E(ref string expression)
        {
            if (expression.StartsWith("(") || Regex.IsMatch(expression, PATTERN_NUMBER))
            {
                T(ref expression);
                Es(ref expression);
            }
        }

        private void T(ref string expression)
        {
            if (expression.StartsWith("(") || Regex.IsMatch(expression, PATTERN_NUMBER))
            {
                P(ref expression);
                Ts(ref expression);
            }
        }

        private void P(ref string expression)
        {
            if (expression.StartsWith("("))
            {
                expression = expression.SkipWord("(");
                E(ref expression);
                if (expression.StartsWith(")"))
                {
                    expression = expression.SkipWord(")");

                    return;
                }
            }

            if (Regex.IsMatch(expression, PATTERN_NUMBER))
            {
                var number = Regex.Match(expression, PATTERN_NUMBER).Value;
                expression = expression.SkipWord(number);
                stack.Push(new ConstantIntExp(Convert.ToInt32(number)));
            }
        }

        private void Es(ref string expression)
        {
            if (expression.StartsWith("+"))
            {
                expression = expression.SkipWord("+");
                T(ref expression);
                var operand1 = stack.Pop();
                var operand2 = stack.Pop();
                var expressionAdd = new AddIntExp(operand1, operand2);
                var resultAdd = new ConstantIntExp(expressionAdd.Evaluate(context));
                stack.Push(resultAdd);
                Es(ref expression);

                return;
            }

            if (expression.StartsWith("-"))
            {
                expression = expression.SkipWord("-");
                T(ref expression);
                var operand1 = stack.Pop();
                var operand2 = stack.Pop();
                var expressionSub = new SubIntExp(operand2, operand1);
                var resultSub = new ConstantIntExp(expressionSub.Evaluate(context));
                stack.Push(resultSub);
                Es(ref expression);

                return;
            }
        }

        private void Ts(ref string expression)
        {
            if (expression.StartsWith("*"))
            {
                expression = expression.SkipWord("*");
                P(ref expression);
                var operand1 = stack.Pop();
                var operand2 = stack.Pop();
                var expressionMulti = new MultiIntExp(operand1, operand2);
                var resultMulti = new ConstantIntExp(expressionMulti.Evaluate(context));
                stack.Push(resultMulti);
                Ts(ref expression);

                return;
            }

            if (expression.StartsWith("/"))
            {
                expression = expression.SkipWord("/");
                P(ref expression);
                var operand1 = stack.Pop();
                var operand2 = stack.Pop();
                var expressionDiv = new DivIntExp(operand2, operand1);
                var resultDiv = new ConstantIntExp(expressionDiv.Evaluate(context));
                stack.Push(resultDiv);
                Ts(ref expression);

                return;
            }
        }
    }
}
