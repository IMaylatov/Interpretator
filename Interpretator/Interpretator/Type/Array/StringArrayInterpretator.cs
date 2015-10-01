using System.Text.RegularExpressions;

namespace Interpretator.Interpretator.Type.Array
{
    class StringArrayInterpretator : IInterpretatorType<string[]>
    {
        private static readonly string PATTERN_VARIABLE_NAME = @"^[a-z]+[0-9]*";

        private Context context;

        private string expression;

        private string[] result;

        public StringArrayInterpretator(Context context)
        {
            this.context = context;
        }

        public string[] Run(string expression)
        {
            this.expression = expression.Trim();

            E();

            return result;
        }

        private void E()
        {
            if (expression.StartsWith("new string["))
            {
                expression = expression.SkipWord("new string[");

                int sizeArray = -1;
                if (Regex.IsMatch(expression, @"^\d"))
                {
                    sizeArray = int.Parse(Regex.Match(expression, @"^\d").Value);
                }
                else if (Regex.IsMatch(expression, PATTERN_VARIABLE_NAME))
                {
                    var nameVariable = Regex.Match(expression, PATTERN_VARIABLE_NAME).Value;
                    var valueVarible = context.Lookup(nameVariable).Value;
                    sizeArray = (int)valueVarible;
                }



                result = new string[sizeArray];
            }
        }
    }
}
