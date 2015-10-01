namespace Interpretator.Type.String
{
    public class ConcatStringExp : Exp<string>
    {
        private Exp<string> operand1, operand2;

        public ConcatStringExp(Exp<string> operand1, Exp<string> operand2)
        {
            this.operand1 = operand1;
            this.operand2 = operand2;
        }

        public string Evaluate(Context c)
        {
            return operand1.Evaluate(c) + operand2.Evaluate(c);
        }

        public Exp<string> Replace(string name, Exp<string> exp)
        {
            return new ConcatStringExp(operand1.Replace(name, exp), operand2.Replace(name, exp));
        }

        public Exp<string> Copy()
        {
            return new ConcatStringExp(operand1, operand2);
        }
    }
}
