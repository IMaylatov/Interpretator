using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Interpretator.Bool;
using Interpretator.Interpretator;
using Interpretator.Type;
using Interpretator.Type.Bool;
using Interpretator.Type.Real;
using Interpretator.Type.String;

namespace Interpretator
{
    public class BoolInterpretator : IInterpretatorType<bool>
    {
        private static readonly string PATTERN_REAL_NUMBER = @"^[0-9]{1}[0-9]*\.[0-9]+";
        private static readonly string PATTERN_INT_NUMBER = @"^\d+";
        private static readonly string PATTERN_VARIABLE_NAME = @"^[a-z]+[0-9]*";

        private Context context;

        private Stack<Exp<Boolean>> stack;


        public BoolInterpretator(Context context)
        {
            this.context = context;
        }


        public bool Run(string expression)
        {
            stack = new Stack<Exp<Boolean>>();

            expression = expression.Trim();

            E(ref expression);

            return stack.Pop().Evaluate(context);
        }


        private void E(ref string expression)
        {
            if (expression.StartsWith("true") || expression.StartsWith("false") || expression.StartsWith("(") || expression.StartsWith("!")
                || expression.StartsWith("\"") || Regex.IsMatch(expression, PATTERN_INT_NUMBER) || Regex.IsMatch(expression, PATTERN_REAL_NUMBER)
                 || Regex.IsMatch(expression, PATTERN_VARIABLE_NAME))
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
                var expressionOr = new OrBooleanExp(operand1, operand2);
                var resultOr = new ConstantBooleanExp(expressionOr.Evaluate(context));
                stack.Push(resultOr);
                Es(ref expression);
            }
        }

        private void T(ref string expression)
        {
            if (expression.StartsWith("true") || expression.StartsWith("false") || expression.StartsWith("(") || expression.StartsWith("!")
                || expression.StartsWith("\"") || Regex.IsMatch(expression, PATTERN_INT_NUMBER) || Regex.IsMatch(expression, PATTERN_REAL_NUMBER)
                 || Regex.IsMatch(expression, PATTERN_VARIABLE_NAME))
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
                var expressionNot = new NotBooleanExp(operand);
                var resultNot = new ConstantBooleanExp(expressionNot.Evaluate(context));
                stack.Push(resultNot);

                return;
            }

            if (expression.StartsWith("true"))
            {
                expression = expression.SkipWord("true");
                stack.Push(new ConstantBooleanExp(true));

                return;
            }

            if (expression.StartsWith("false"))
            {
                expression = expression.SkipWord("false");
                stack.Push(new ConstantBooleanExp(false));

                return;
            }

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

            if (expression.StartsWith("\""))
            {
                var stringInterpretator = new StringInterpretator(context);
                if (expression.Contains("=="))
                {
                    var leftOperand = expression.Substring(0, expression.IndexOf("=="));
                    expression = expression.SkipWord(leftOperand);
                    expression = expression.SkipWord("==");
                    string rightOperand = string.Empty;
                    while ((expression.Length != 0) && !(expression.StartsWith("&&") || expression.StartsWith("||")))
                    {
                        rightOperand += expression[0];
                        expression = expression.SkipWord(expression[0].ToString());
                    }

                    var resultLeftOperand = stringInterpretator.Run(leftOperand);
                    var resultRightOperand = stringInterpretator.Run(rightOperand);

                    var stringExpressionEquals = new StringEqualsBooleanExp(new ConstantStringExp(resultLeftOperand), new ConstantStringExp(resultRightOperand));
                    var resultStringEquals = stringExpressionEquals.Evaluate(context);

                    stack.Push(new ConstantBooleanExp(resultStringEquals));
                }
                else if (expression.Contains("!="))
                {
                    var leftOperand = expression.Substring(0, expression.IndexOf("!="));
                    expression = expression.SkipWord(leftOperand);
                    expression = expression.SkipWord("!=");
                    string rightOperand = string.Empty;
                    while ((expression.Length != 0) && !(expression.StartsWith("&&") || expression.StartsWith("||")))
                    {
                        rightOperand += expression[0];
                        expression = expression.SkipWord(expression[0].ToString());
                    }

                    var resultLeftOperand = stringInterpretator.Run(leftOperand);
                    var resultRightOperand = stringInterpretator.Run(rightOperand);

                    var stringExpressionEquals = new StringNotEqualsBooleanExp(new ConstantStringExp(resultLeftOperand), new ConstantStringExp(resultRightOperand));
                    var resultStringEquals = stringExpressionEquals.Evaluate(context);

                    stack.Push(new ConstantBooleanExp(resultStringEquals));
                }

                return;
            }

            if (Regex.IsMatch(expression, PATTERN_INT_NUMBER) || Regex.IsMatch(expression, PATTERN_REAL_NUMBER) || Regex.IsMatch(expression, PATTERN_VARIABLE_NAME))
            {
                var realInterpretator = new RealInterpretator(context);

                string leftOperand = string.Empty;
                if (!Regex.IsMatch(expression, PATTERN_VARIABLE_NAME))
                {
                    while (!(expression.StartsWith("==") || expression.StartsWith("!=") || expression.StartsWith(">=") || expression.StartsWith(">") || 
                        expression.StartsWith("<=") || expression.StartsWith("<")))
                    {
                        leftOperand += expression[0];
                        expression = expression.SkipWord(expression[0].ToString());
                    }
                }
                else
                {
                    leftOperand = Regex.Match(expression, PATTERN_VARIABLE_NAME).Value;
                    expression = expression.SkipWord(leftOperand);
                }

                var operation = expression[1] == '=' ? expression.Substring(0, 2) : expression.Substring(0, 1);
                expression = expression.SkipWord(operation);
                var rightOperand = FindRightOperand(ref expression);
                
                double resultLeftOperand = 0;
                if (Regex.IsMatch(leftOperand, PATTERN_VARIABLE_NAME))
                {
                    var variableValue = context.Lookup(leftOperand).Value;
                    if (variableValue is int)
                    {
                        resultLeftOperand = (int)variableValue;
                    }
                    else if (variableValue is double)
                    {
                        resultLeftOperand = (double)variableValue;
                    }
                }
                else
                {
                    resultLeftOperand = realInterpretator.Run(leftOperand);
                }
                double resultRightOperand = 0;
                if (Regex.IsMatch(rightOperand, PATTERN_VARIABLE_NAME))
                {
                    var variableValue = context.Lookup(rightOperand).Value;
                    if (variableValue is int)
                    {
                        resultRightOperand = (int)variableValue;
                    }
                    else if (variableValue is double)
                    {
                        resultRightOperand = (double)variableValue;
                    }
                }
                else
                {
                    resultRightOperand = realInterpretator.Run(rightOperand);
                }

                Exp<bool> expBool = null;

                switch (operation)
                {
                    case "==":
                        expBool = new RealEqualsBooleanExp(new ConstantRealExp(resultLeftOperand), new ConstantRealExp(resultRightOperand));
                        break;
                    case "!=":
                        expBool = new RealNotEqualsBooleanExp(new ConstantRealExp(resultLeftOperand), new ConstantRealExp(resultRightOperand));
                        break;
                    case ">=":
                        expBool = new RealMoreOrEqualsBooleanExp(new ConstantRealExp(resultLeftOperand), new ConstantRealExp(resultRightOperand));
                        break;
                    case ">":
                        expBool = new RealMoreBooleanExp(new ConstantRealExp(resultLeftOperand), new ConstantRealExp(resultRightOperand));
                        break;
                    case "<=":
                        expBool = new RealLessOrEqualsBooleanExp(new ConstantRealExp(resultLeftOperand), new ConstantRealExp(resultRightOperand));
                        break;
                    case "<":
                        expBool = new RealLessBooleanExp(new ConstantRealExp(resultLeftOperand), new ConstantRealExp(resultRightOperand));
                        break;
                }

                var resultExpBool = expBool.Evaluate(context);

                stack.Push(new ConstantBooleanExp(resultExpBool));
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
                var expressionAnd = new AndBooleanExp(operand1, operand2);
                var resultAnd = new ConstantBooleanExp(expressionAnd.Evaluate(context));
                stack.Push(resultAnd);
                Ts(ref expression);
            }
        }

        private string FindRightOperand(ref string expression)
        {
            string rightOperand = string.Empty;
            if (!Regex.IsMatch(expression, PATTERN_VARIABLE_NAME))
            {
                while ((expression.Length != 0) && !(expression.StartsWith("&&") || expression.StartsWith("||")))
                {
                    rightOperand += expression[0];
                    expression = expression.SkipWord(expression[0].ToString());
                }
            }
            else
            {
                rightOperand = Regex.Match(expression, PATTERN_VARIABLE_NAME).Value;
                expression = expression.SkipWord(rightOperand);
            }
            return rightOperand;
        }
    }
}
