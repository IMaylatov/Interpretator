using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpretator.Bool
{
    public class VariableExp : BooleanExp 
    {
        public string Name { get; private set; }

        public VariableExp(String name)
        {
            Name = name;
	    }
        
	    public bool Evaluate(Context c) {
            return c.Lookup(Name);
	    }

	    public BooleanExp Copy() {
            return new VariableExp(Name);
	    }
	
	    public BooleanExp Replace(String name, BooleanExp exp) 
        {
            if (Name == name)
            {
			    return exp.Copy();
		    } 
            else 
            {
			    return Copy();
		    }
	    }
    }
}
