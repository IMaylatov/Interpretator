using System;
using Interpretator.Type;

namespace Interpretator.Bool
{
    public class AndBooleanExp : Exp<Boolean> 
    {
        private Exp<Boolean> operand1, operand2;

        public AndBooleanExp(Exp<Boolean> operand1, Exp<Boolean> operand2) 
        {
		    this.operand1 = operand1;
		    this.operand2 = operand2;
	    }

	    public bool Evaluate(Context c) 
        {
		    return operand1.Evaluate(c) && operand2.Evaluate(c);
	    }

        public Exp<Boolean> Replace(String str, Exp<Boolean> exp)
        {
		    return new AndBooleanExp(operand1.Replace(str, exp), operand2.Replace(str, exp));
	    }

        public Exp<Boolean> Copy() 
        {
		    return new AndBooleanExp(operand1.Copy(), operand2.Copy());
	    }
    }
}
