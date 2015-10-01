using System;
using Interpretator.Type;

namespace Interpretator.Bool
{
    public class ConstantBooleanExp : Exp<Boolean> 
    {
	    private bool constant;
	
	    public ConstantBooleanExp(bool constant) 
        {
		    this.constant = constant;
	    }

        public Exp<Boolean> Copy()
        {
		    return new ConstantBooleanExp(this.constant);
	    }

        public bool Evaluate(Context c)
        {
		    return this.constant;
	    }

        public Exp<Boolean> Replace(String str, Exp<Boolean> exp) 
        {
		    return Copy();
	    }

    }
}
