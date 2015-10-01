namespace Interpretator.Type.Real
{
    public class AddRealExp : Exp<double>
    {
        private Exp<double> operand1, operand2;

        public AddRealExp(Exp<double> operand1, Exp<double> operand2)
        {
            this.operand1 = operand1;
            this.operand2 = operand2;
        }

        public double Evaluate(Context c)
        {
            return operand1.Evaluate(c) + operand2.Evaluate(c);
        }

        public Exp<double> Replace(string name, Exp<double> exp)
        {
            return new AddRealExp(operand1.Replace(name, exp), operand2.Replace(name, exp));
        }

        public Exp<double> Copy()
        {
            return new AddRealExp(operand1.Copy(), operand2.Copy());
        }
    }
}
