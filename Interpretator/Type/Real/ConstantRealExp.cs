using System;

namespace Interpretator.Type.Real
{
    public class ConstantRealExp : Exp<double>
    {
        private Double constant;

        public ConstantRealExp(double constant) 
        {
		    this.constant = constant;
	    }

        public double Evaluate(Context c)
        {
            return constant;
        }

        public Exp<double> Replace(string name, Exp<double> exp)
        {
            return Copy();
        }

        public Exp<double> Copy()
        {
            return new ConstantRealExp(constant);
        }
    }
}
