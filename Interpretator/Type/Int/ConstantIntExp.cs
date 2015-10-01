namespace Interpretator.Type.Int
{
    class ConstantIntExp : Exp<int>
    {
        private int constant;

        public ConstantIntExp(int constant) 
        {
		    this.constant = constant;
	    }

        public int Evaluate(Context c)
        {
            return constant;
        }

        public Exp<int> Replace(string name, Exp<int> exp)
        {
            return Copy();
        }

        public Exp<int> Copy()
        {
            return new ConstantIntExp(constant);
        }
    }
}
