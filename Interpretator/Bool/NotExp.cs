using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpretator.Bool
{
    public class NotExp : BooleanExp 
    {
	    private BooleanExp operand;
 
	    public NotExp(BooleanExp operand) 
        {
		    this.operand = operand;
	    }
 
	    public bool Evaluate(Context c) 
        {
		    return !operand.Evaluate(c);
	    }
 
	    public BooleanExp Replace(String str, BooleanExp exp)
        {
		    return new NotExp(operand.Replace(str, exp));
	    }
 
	    public BooleanExp Copy() 
        {
		    return new NotExp(operand.Copy());
	    }
    }
}
