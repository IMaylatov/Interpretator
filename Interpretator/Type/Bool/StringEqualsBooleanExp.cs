namespace Interpretator.Type.Bool
{
    public class StringEqualsBooleanExp : Exp<bool>
    {
        private Exp<string> operand1, operand2;

        public StringEqualsBooleanExp(Exp<string> operand1, Exp<string> operand2)
        {
            this.operand1 = operand1;
            this.operand2 = operand2;
        }

        public bool Evaluate(Context c)
        {
            return operand1.Evaluate(c) == operand2.Evaluate(c);
        }

        public Exp<bool> Replace(string name, Exp<bool> exp)
        {
            return null;
        }

        public Exp<bool> Copy()
        {
            return null;
        }
    }
}
