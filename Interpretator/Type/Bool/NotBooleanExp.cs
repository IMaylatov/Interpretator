using System;
using Interpretator.Type;

namespace Interpretator.Bool
{
    public class NotBooleanExp : Exp<Boolean> 
    {
        private Exp<Boolean> operand;

        public NotBooleanExp(Exp<Boolean> operand) 
        {
		    this.operand = operand;
	    }
 
	    public bool Evaluate(Context c) 
        {
		    return !operand.Evaluate(c);
	    }

        public Exp<Boolean> Replace(String str, Exp<Boolean> exp)
        {
		    return new NotBooleanExp(operand.Replace(str, exp));
	    }

        public Exp<Boolean> Copy() 
        {
		    return new NotBooleanExp(operand.Copy());
	    }
    }
}
