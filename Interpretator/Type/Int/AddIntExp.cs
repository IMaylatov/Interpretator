﻿namespace Interpretator.Type.Int
{
    public class AddIntExp : Exp<int>
    {
        private Exp<int> operand1, operand2;

        public AddIntExp(Exp<int> operand1, Exp<int> operand2)
        {
            this.operand1 = operand1;
            this.operand2 = operand2;
        }

        public int Evaluate(Context c)
        {
            return operand1.Evaluate(c) + operand2.Evaluate(c);
        }

        public Exp<int> Replace(string name, Exp<int> exp)
        {
            return new AddIntExp(operand1.Replace(name, exp), operand2.Replace(name, exp));
        }

        public Exp<int> Copy()
        {
            return new AddIntExp(operand1.Copy(), operand2.Copy());
        }
    }
}
