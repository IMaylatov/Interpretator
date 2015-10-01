using System.Text.RegularExpressions;

namespace Interpretator.Interpretator.Type.Array
{
    public class BoolArrayInterpretator : IInterpretatorType<bool[]>
    {
        private static readonly string PATTERN_VARIABLE_NAME = @"^[a-z]+[0-9]*";

        private Context context;

        private string expression;

        private bool[] result;

        public BoolArrayInterpretator(Context context)
        {
            this.context = context;
        }

        public bool[] Run(string expression)
        {
            this.expression = expression.Trim();

            E();

            return result;
        }

        private void E()
        {
            if (expression.StartsWith("new bool["))
            {
                expression = expression.SkipWord("new bool[");

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



                result = new bool[sizeArray];
            }
        }
    }
}
