using System;

namespace Interpretator.Bool
{
    public class ConstantExp : BooleanExp 
    {
	    private bool constant;
	
	    public ConstantExp(bool constant) 
        {
		    this.constant = constant;
	    }

	    public BooleanExp Copy()
        {
		    return new ConstantExp(this.constant);
	    }

        public bool Evaluate(Context c)
        {
		    return this.constant;
	    }

	    public BooleanExp Replace(String str, BooleanExp exp) 
        {
		    return Copy();
	    }

    }
}
