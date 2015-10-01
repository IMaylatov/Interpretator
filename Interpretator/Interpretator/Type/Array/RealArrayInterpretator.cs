using System;
using System.Text.RegularExpressions;

namespace Interpretator.Interpretator.Type.Array
{
    public class RealArrayInterpretator : IInterpretatorType<double[]>
    {
        private static readonly string PATTERN_VARIABLE_NAME = @"^[a-z]+[0-9]*";

        private Context context;

        private string expression;

        private double[] result;

        public RealArrayInterpretator(Context context)
        {
            this.context = context;
        }

        public double[] Run(string expression)
        {
            this.expression = expression.Trim();

            E();

            return result;
        }

        private void E()
        {
            if (expression.StartsWith("new real["))
            {
                expression = expression.SkipWord("new real[");

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



                result = new double[sizeArray];
            }
        }
    }
}
