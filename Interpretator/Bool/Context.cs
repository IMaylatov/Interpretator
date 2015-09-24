using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpretator.Bool
{
    public class Context
    {
        Dictionary<String, Boolean> vars = new Dictionary<String, Boolean>();

        public bool Lookup(String name)
        {
            bool result;
            if (vars.TryGetValue(name, out result))
            {
                return result;
            }
            return false;
        }

        public void Assign(VariableExp exp, bool val)
        {
            vars.Add(exp.Name, val);
        }
    }
}
