using System.Text.RegularExpressions;

namespace Interpretator.Interpretator
{
    public class RegionInterpretator
    {
        private static readonly string PATTERN_VARIABLE_NAME = @"^[a-z]+[0-9]*";

        private Context context;

        private string expression;

        public RegionInterpretator(Context context)
        {
            this.context = context;
        }

        public void Run(string expression)
        {
            this.expression = expression.Trim();

            E();
        }

        private void E()
        {
            if (expression.StartsWith("int") || expression.StartsWith("real") || expression.StartsWith("bool") || expression.StartsWith("string")
                || Regex.IsMatch(expression, PATTERN_VARIABLE_NAME))
            {
                var variableInterpretator = new VariableInterpretator(context);
                var expressionString = Regex.Match(expression, "^.+?;").Value;
                variableInterpretator.Run(expressionString);

                expression = expression.SkipWord(expressionString);

                E();
            }
        }
    }
}
