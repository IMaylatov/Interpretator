﻿namespace Interpretator.Type.Bool
{
    class RealEqualsBooleanExp : Exp<bool>
    {
        private Exp<double> operand1, operand2;

        public RealEqualsBooleanExp(Exp<double> operand1, Exp<double> operand2)
        {
            this.operand1 = operand1;
            this.operand2 = operand2;
        }

        public bool Evaluate(Context c)
        {
            return operand1.Evaluate(c) == operand2.Evaluate(c);
        }

        public Exp<bool> Replace(string name, Exp<bool> exp)
        {
            return null;
        }

        public Exp<bool> Copy()
        {
            return null;
        }
    }
}
