using System.Text.RegularExpressions;

namespace Interpretator.Interpretator.Type.Array
{
    public class IntArrayInterpretator : IInterpretatorType<int[]>
    {
        private static readonly string PATTERN_VARIABLE_NAME = @"^[a-z]+[0-9]*";

        private Context context;

        private string expression;

        private int[] result;

        public IntArrayInterpretator(Context context)
        {
            this.context = context;
        }

        public int[] Run(string expression)
        {
            this.expression = expression.Trim();

            E();

            return result;
        }

        private void E()
        {
            if (expression.StartsWith("new int["))
            {
                expression = expression.SkipWord("new int[");

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



                result = new int[sizeArray];
            }
        }
    }
}
