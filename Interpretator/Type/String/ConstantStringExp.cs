namespace Interpretator.Type.String
{
    class ConstantStringExp : Exp<string>
    {
        private string constant;

        public ConstantStringExp(string constant)
        {
            this.constant = constant;
        }

        public string Evaluate(Context c)
        {
            return constant;
        }

        public Exp<string> Replace(string name, Exp<string> exp)
        {
            return Copy();
        }

        public Exp<string> Copy()
        {
            return new ConstantStringExp(constant);
        }
    }
}
