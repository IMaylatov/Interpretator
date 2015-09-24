using System;
using System.Collections.Generic;
using Interpretator.Bool;

namespace Interpretator
{
    public class BooleanExpInterpretator : IInterpretator<BooleanExp>
    {
        private Stack<BooleanExp> stack;

        private Context context;

        public BooleanExpInterpretator(Context context)
        {
            this.context = context;
        }

        public BooleanExp Run(string expression)
        {
            stack = new Stack<BooleanExp>();

            expression = expression.Trim();

            E(ref expression);

            return stack.Pop();
        }

        private void E(ref string expression)
        {
            if (expression.StartsWith("true") || expression.StartsWith("false") || expression.StartsWith("(") || expression.StartsWith("!"))
            {
                T(ref expression);
                Es(ref expression);
            }
        }

        private void Es(ref string expression)
        {
            if (expression.StartsWith("||"))
            {
                expression = expression.SkipWord("||");
                T(ref expression);
                var operand1 = stack.Pop();
                var operand2 = stack.Pop();
                var expressionOr = new OrExp(operand1, operand2);
                var resultOr = new ConstantExp(expressionOr.Evaluate(context));
                stack.Push(resultOr);
                Es(ref expression);
            }
        }

        private void T(ref string expression)
        {
            if (expression.StartsWith("true") || expression.StartsWith("false") || expression.StartsWith("(") || expression.StartsWith("!"))
            {
                P(ref expression);
                Ts(ref expression);
            } 
        }

        private void P(ref string expression)
        {
            if (expression.StartsWith("!"))
            {
                expression = expression.SkipWord("!");
                P(ref expression);
                var operand = stack.Pop();
                var expressionNot = new NotExp(operand);
                var resultNot = new ConstantExp(expressionNot.Evaluate(context));
                stack.Push(resultNot);

                return;
            }

            if (expression.StartsWith("true"))
            {
                expression = expression.SkipWord("true");
                stack.Push(new ConstantExp(true));

                return;
            }

            if (expression.StartsWith("false"))
            {
                expression = expression.SkipWord("false");
                stack.Push(new ConstantExp(false));

                return;
            }

            if (expression.StartsWith("("))
            {
                expression = expression.SkipWord("(");
                E(ref expression);
                if (expression.StartsWith(")"))
                {
                    expression = expression.SkipWord(")");
                }
            }
        }

        private void Ts(ref string expression)
        {
            if (expression.StartsWith("&&"))
            {
                expression = expression.SkipWord("&&");
                P(ref expression);
                var operand1 = stack.Pop();
                var operand2 = stack.Pop();
                var expressionAnd = new AndExp(operand1, operand2);
                var resultAnd = new ConstantExp(expressionAnd.Evaluate(context));
                stack.Push(resultAnd);
                Ts(ref expression);
            }
        }
    }
}
