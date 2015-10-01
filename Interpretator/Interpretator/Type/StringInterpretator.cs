using System.Collections.Generic;
using System.Text.RegularExpressions;
using Interpretator.Type;
using Interpretator.Type.String;

namespace Interpretator.Interpretator
{
    public class StringInterpretator : IInterpretatorType<string>
    {
        private static readonly string PATTERN_STRING = "^\".*?\"";
        private static readonly string PATTERN_VARIABLE_NAME = @"^[a-z]+[0-9]*";

        private Context context;

        private Stack<Exp<string>> stack;


        public StringInterpretator(Context context)
        {
            this.context = context;
        }

        public string Run(string expression)
        {
            stack = new Stack<Exp<string>>();

            expression = expression.Trim();

            E(ref expression);

            return stack.Pop().Evaluate(context);
        }

        private void E(ref string expression)
        {
            if (Regex.IsMatch(expression, PATTERN_STRING) || Regex.IsMatch(expression, PATTERN_VARIABLE_NAME))
            {
                T(ref expression);
                Es(ref expression);
            }
        }

        private void T(ref string expression)
        {
            if (Regex.IsMatch(expression, PATTERN_STRING))
            {
                var findString = Regex.Match(expression, PATTERN_STRING).Value;
                expression = expression.SkipWord(findString);
                stack.Push(new ConstantStringExp(findString.Trim('"')));

                return;
            }

            if (Regex.IsMatch(expression, PATTERN_VARIABLE_NAME))
            {
                var nameVariable = Regex.Match(expression, PATTERN_VARIABLE_NAME).Value;
                expression = expression.SkipWord(nameVariable);
                var variable = context.Lookup(nameVariable);
                stack.Push(new ConstantStringExp((string)variable.Value));
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
                var expressionConcat = new ConcatStringExp(operand2, operand1);
                var resultConcat = new ConstantStringExp(expressionConcat.Evaluate(context));
                stack.Push(resultConcat);
                Es(ref expression);
            }
        }

    }
}
