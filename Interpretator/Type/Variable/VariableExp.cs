using Interpretator.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpretator.Type.Variable
{
    public class VariableExp
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public VariableExp(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }

        public object Evaluate(Context c)
        {
            return c.Lookup(Name).Value;
        }

        public object Replace(string name, Exp<object> exp)
        {
            if (this.Name == name)
            {
                return exp.Copy();
            }
            else
            {
                return Copy();
            }
        }

        public object Copy()
        {
            return new VariableExp(Name, Value);
        }
    }
}
