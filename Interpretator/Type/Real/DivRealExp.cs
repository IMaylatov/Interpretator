namespace Interpretator.Type.Real
{
    public class DivRealExp : Exp<double>
    {
        private Exp<double> operand1, operand2;

        public DivRealExp(Exp<double> operand1, Exp<double> operand2)
        {
            this.operand1 = operand1;
            this.operand2 = operand2;
        }

        public double Evaluate(Context c)
        {
            return operand1.Evaluate(c)/operand2.Evaluate(c);
        }

        public Exp<double> Replace(string name, Exp<double> exp)
        {
            return new DivRealExp(operand1.Replace(name, exp), operand2.Replace(name, exp));
        }

        public Exp<double> Copy()
        {
            return new DivRealExp(operand1.Copy(), operand2.Copy());
        }
    }
}
