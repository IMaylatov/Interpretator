using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpretator.Bool
{
    public class OrExp : BooleanExp 
    {
	    private BooleanExp operand1, operand2;
	
	    public OrExp(BooleanExp operand1, BooleanExp operand2)
        {
		    this.operand1 = operand1;
		    this.operand2 = operand2;
	    }

	    public bool Evaluate(Context c) 
        {
		    return operand1.Evaluate(c) || operand2.Evaluate(c);
	    }
	
	    public BooleanExp Replace(String str, BooleanExp exp) 
        {
		    return new OrExp(operand1.Replace(str, exp), operand2.Replace(str, exp));
	    }
	
	    public BooleanExp Copy() 
        {
		    return new OrExp(operand1.Copy(), operand2.Copy());
	    }
    }
}
