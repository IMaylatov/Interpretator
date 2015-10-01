using System;
using System.Collections.Generic;
using Interpretator.Type.Variable;

namespace Interpretator
{
    public class Context
    {
        Dictionary<String, VariableExp> vars = new Dictionary<String, VariableExp>();

        public VariableExp Lookup(String name)
        {
            VariableExp result;
            if (vars.TryGetValue(name, out result))
            {
                return result;
            }
            return result;
        }

        public void AddVariable(VariableExp variableExp)
        {
            vars.Add(variableExp.Name, variableExp);
        }
    }
}
