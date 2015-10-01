using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Interpretator.Type;
using Interpretator.Type.Real;

namespace Interpretator.Interpretator
{
    public class RealInterpretator : IInterpretatorType<double>
    {
        private static readonly string PATTERN_REAL_NUMBER = @"^[0-9]{1}[0-9]*\.[0-9]+";
        private static readonly string PATTERN_INT_NUMBER = @"^\d+";
        private static readonly string PATTERN_VARIABLE_NAME = @"^[a-z]+[0-9]*";


        private Context context;

        private Stack<Exp<double>> stack;


        public RealInterpretator(Context context)
        {
            this.context = context;
        }

        public double Run(string expression)
        {
            stack = new Stack<Exp<double>>();

            expression = expression.Trim();

            E(ref expression);

            return stack.Pop().Evaluate(context);
        }

        private void E(ref string expression)
        {
            if (expression.StartsWith("(") || Regex.IsMatch(expression, PATTERN_REAL_NUMBER) || Regex.IsMatch(expression, PATTERN_INT_NUMBER) || Regex.IsMatch(expression, PATTERN_VARIABLE_NAME))
            {
                T(ref expression);
                Es(ref expression);
            }
        }

        private void T(ref string expression)
        {
            if (expression.StartsWith("(") || Regex.IsMatch(expression, PATTERN_REAL_NUMBER) || Regex.IsMatch(expression, PATTERN_INT_NUMBER) || Regex.IsMatch(expression, PATTERN_VARIABLE_NAME))
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

            if (Regex.IsMatch(expression, PATTERN_REAL_NUMBER))
            {
                var number = Regex.Match(expression, PATTERN_REAL_NUMBER).Value;
                expression = expression.SkipWord(number);
                stack.Push(new ConstantRealExp(double.Parse(number.Replace(".", ","))));

                return;
            }

            if (Regex.IsMatch(expression, PATTERN_INT_NUMBER))
            {
                var interpretatorInt = new IntInterpretator(context);
                var number = Regex.Match(expression, PATTERN_INT_NUMBER).Value;
                var resultInterpretatorNumber = interpretatorInt.Run(number);
                expression = expression.SkipWord(number);
                stack.Push(new ConstantRealExp(resultInterpretatorNumber));

                return;

            }

            if (Regex.IsMatch(expression, PATTERN_VARIABLE_NAME))
            {
                var nameVariable = Regex.Match(expression, PATTERN_VARIABLE_NAME).Value;
                expression = expression.SkipWord(nameVariable);
                var variable = context.Lookup(nameVariable);
                stack.Push(new ConstantRealExp((int)variable.Value));
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
                var expressionAdd = new AddRealExp(operand1, operand2);
                var resultAdd = new ConstantRealExp(expressionAdd.Evaluate(context));
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
                var expressionSub = new SubRealExp(operand2, operand1);
                var resultSub = new ConstantRealExp(expressionSub.Evaluate(context));
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
                var expressionMulti = new MultiRealExp(operand1, operand2);
                var resultMulti = new ConstantRealExp(expressionMulti.Evaluate(context));
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
                var expressionDiv = new DivRealExp(operand2, operand1);
                var resultDiv = new ConstantRealExp(expressionDiv.Evaluate(context));
                stack.Push(resultDiv);
                Ts(ref expression);

                return;
            }
        }
    }
}
