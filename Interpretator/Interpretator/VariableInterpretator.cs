using System.Collections.Generic;
using Interpretator.Type.Variable;
using System.Text.RegularExpressions;
using Interpretator.Interpretator.Type.Array;

namespace Interpretator.Interpretator
{
    public class VariableInterpretator
    {
        private static readonly string PATTERN_VARIABLE_NAME = @"^[a-z]+[0-9]*";

        private Context context;

        private Stack<object> stack;

        private string expression;

        public VariableInterpretator(Context context)
        {
            this.context = context;
        }

        public void Run(string expression)
        {
            stack = new Stack<object>();

            this.expression = expression.Trim();

            E();
        }

        private void E()
        {
            if (expression.StartsWith("int[]"))
            {
                expression = expression.SkipWord("int[]");

                N();

                var intArrayInterpretator = new IntArrayInterpretator(context);
                var expressionString = Regex.Match(expression, "^.+;").Value;
                var valueVariable = intArrayInterpretator.Run(expression.Remove(expressionString.Length - 1, 1));

                expression = expression.SkipWord(expressionString);

                var variableName = (string) stack.Pop();
                context.AddVariable(new VariableExp(variableName, valueVariable));

                return;
            }

            if (expression.StartsWith("real[]"))
            {
                expression = expression.SkipWord("real[]");

                N();

                var realArrayInterpretator = new RealArrayInterpretator(context);
                var expressionString = Regex.Match(expression, "^.+;").Value;
                var valueVariable = realArrayInterpretator.Run(expression.Remove(expressionString.Length - 1, 1));

                expression = expression.SkipWord(expressionString);

                var variableName = (string)stack.Pop();
                context.AddVariable(new VariableExp(variableName, valueVariable));

                return;
            }

            if (expression.StartsWith("bool[]"))
            {
                expression = expression.SkipWord("bool[]");

                N();

                var boolArrayInterpretator = new BoolArrayInterpretator(context);
                var expressionString = Regex.Match(expression, "^.+;").Value;
                var valueVariable = boolArrayInterpretator.Run(expression.Remove(expressionString.Length - 1, 1));

                expression = expression.SkipWord(expressionString);

                var variableName = (string)stack.Pop();
                context.AddVariable(new VariableExp(variableName, valueVariable));

                return;
            }

            if (expression.StartsWith("string[]"))
            {
                expression = expression.SkipWord("string[]");

                N();

                var stringArrayInterpretator = new StringArrayInterpretator(context);
                var expressionString = Regex.Match(expression, "^.+;").Value;
                var valueVariable = stringArrayInterpretator.Run(expression.Remove(expressionString.Length - 1, 1));

                expression = expression.SkipWord(expressionString);

                var variableName = (string)stack.Pop();
                context.AddVariable(new VariableExp(variableName, valueVariable));

                return;
            }

            if (expression.StartsWith("int"))
            {
                expression = expression.SkipWord("int");

                N();

                var intInterpratator = new IntInterpretator(context);
                var expressionString = Regex.Match(expression, "^.+;").Value;
                var valueVariable = intInterpratator.Run(expressionString.Remove(expressionString.Length - 1, 1));

                expression = expression.SkipWord(expressionString);

                var variableName = (string)stack.Pop();
                context.AddVariable(new VariableExp(variableName, valueVariable));

                return;
            }

            if (expression.StartsWith("real"))
            {
                expression = expression.SkipWord("real");

                N();

                var realInterpretator = new RealInterpretator(context);
                var expressionString = Regex.Match(expression, "^.+;").Value;
                var valueVariable = realInterpretator.Run(expression.Remove(expressionString.Length - 1, 1));

                expression = expression.SkipWord(expressionString);

                var variableName = (string) stack.Pop();
                context.AddVariable(new VariableExp(variableName, valueVariable));

                return;
            }

            if (expression.StartsWith("bool"))
            {
                expression = expression.SkipWord("bool");

                N();

                var boolInterpretator = new BoolInterpretator(context);
                var expressionString = Regex.Match(expression, "^.+").Value;
                var valueVariable = boolInterpretator.Run(expression.Remove(expressionString.Length - 1, 1));

                expression = expression.SkipWord(expressionString);

                var variableName = (string) stack.Pop();
                context.AddVariable(new VariableExp(variableName, valueVariable));

                return;
            }

            if (expression.StartsWith("string"))
            {
                expression = expression.SkipWord("string");

                N();

                var stringInterpretator = new StringInterpretator(context);
                var expressionString = Regex.Match(expression, "^.+").Value;
                var valueVariable = stringInterpretator.Run(expression.Remove(expressionString.Length - 1, 1));

                expression = expression.SkipWord(expressionString);

                var variableName = (string) stack.Peek();
                context.AddVariable(new VariableExp(variableName, valueVariable));
            }

            if (Regex.IsMatch(expression, PATTERN_VARIABLE_NAME))
            {
                N();

                var variableName = (string)stack.Peek();
                var variable = context.Lookup(variableName);
                string expressionNewValue = null;

                if (variable.Value is int)
                {
                    var interpretatorInt = new IntInterpretator(context);
                    expressionNewValue = Regex.Match(expression, "^.+").Value.Replace(";", string.Empty);
                    variable.Value = interpretatorInt.Run(expressionNewValue);
                }
                else if (variable.Value is double)
                {
                    var interpretatorReal = new RealInterpretator(context);
                    expressionNewValue = Regex.Match(expression, "^.+").Value.Replace(";", string.Empty);
                    variable.Value = interpretatorReal.Run(expressionNewValue);
                }
                else if (variable.Value is bool)
                {
                    var interpretatorBool = new BoolInterpretator(context);
                    expressionNewValue = Regex.Match(expression, "^.+").Value.Replace(";", string.Empty);
                    variable.Value = interpretatorBool.Run(expressionNewValue);
                }else if (variable.Value is string)
                {
                    var interpretatorString = new StringInterpretator(context);
                    expressionNewValue = Regex.Match(expression, "^.+").Value.Replace(";", string.Empty);
                    variable.Value = interpretatorString.Run(expressionNewValue);
                }

                expression = expression.SkipWord(expressionNewValue);
            }
        }

        private void N()
        {
            string variableName = string.Empty;
            while (expression.Length != 0 && !expression.StartsWith("="))
            {
                variableName += expression[0];
                expression = expression.SkipWord(expression[0].ToString());
            }
            stack.Push(variableName);
            expression = expression.SkipWord("=");
        }
    }
}
