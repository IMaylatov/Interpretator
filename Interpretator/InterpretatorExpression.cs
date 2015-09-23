using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Interpretator
{
    public class InterpretatorExpression : IInterpretator
    {
        private List<string> elements = new List<string>();

        private Regex regexInt = new Regex(@"^-?\d+$");
        private Regex regexPlus = new Regex(@"[+]");
        private Regex regexMulty = new Regex(@"[*]");
        private Regex regexLeftBracket = new Regex(@"[(]$");
        private Regex regexRightBracket = new Regex(@"[)]$");

        public object Run(string expression)
        {
            elements.Clear();

            var enumeratorText = expression.GetEnumerator();

            enumeratorText.MoveNext();
            E(enumeratorText);

            Stack<string> stack = new Stack<string>();
            foreach (var element in elements)
            {
                if (!IsOperation(element))
                {
                    stack.Push(element);
                }
                else
                {
                    ExecuteOperation(element, stack);
                }
            }

            return stack.Pop();
        }
        
        private void E(CharEnumerator enumerator)
        {
            var readedSymbols = enumerator.Current.ToString();
            if (regexInt.IsMatch(readedSymbols))
            {
                T(enumerator);
                Es(enumerator);

                return;
            }
            else if (regexLeftBracket.IsMatch(readedSymbols))
            {
                T(enumerator);
                Es(enumerator);

                return;
            }

            throw new ArgumentException("Неправильный синтаксис");
        }

        private void Es(CharEnumerator enumerator)
        {
            if (!IsEmpty(enumerator))
            {
                var readedSymbols = enumerator.Current.ToString();
                if (regexPlus.IsMatch(readedSymbols))
                {
                    enumerator.MoveNext();
                    T(enumerator);
                    elements.Add("+");
                    Es(enumerator);
                }
            }
        }

        private void T(CharEnumerator enumerator)
        {
            var readedSymbols = enumerator.Current.ToString();
            if (regexInt.IsMatch(readedSymbols))
            {
                P(enumerator);
                Ts(enumerator);

                return;
            }
            else if (regexLeftBracket.IsMatch(readedSymbols))
            {
                P(enumerator);
                Ts(enumerator);

                return;
            }

            throw new ArgumentException("Неправильный синтаксис");
        }

        private void Ts(CharEnumerator enumerator)
        {
            if (IsEmpty(enumerator))
            {
                return;
            }
            else
            {
                var readedSymbols = enumerator.Current.ToString();
                if (regexMulty.IsMatch(readedSymbols))
                {
                    enumerator.MoveNext();
                    P(enumerator);
                    elements.Add("*");
                    Ts(enumerator);
                }

                return;
            }

            throw new ArgumentException("Неправильный синтаксис");
        }

        private void P(CharEnumerator enumerator)
        {
            var readedSymbols = enumerator.Current.ToString();
            if (regexInt.IsMatch(readedSymbols))
            {
                while ((enumerator.MoveNext()) && (regexInt.IsMatch(readedSymbols + enumerator.Current)))
                {
                    readedSymbols += enumerator.Current;
                }
                elements.Add(readedSymbols);

                return;
            }
            else if (regexLeftBracket.IsMatch(readedSymbols))
            {
                enumerator.MoveNext();
                E(enumerator);
                readedSymbols = enumerator.Current.ToString();
                if (regexRightBracket.IsMatch(readedSymbols))
                {
                    enumerator.MoveNext();

                    return;
                }
            }

            throw new ArgumentException("Неправильный синтаксис");
        }

        private bool IsEmpty(CharEnumerator enumerator)
        {
            try
            {
                var tryGetCurent = enumerator.Current;
                return false;
            }
            catch (Exception e)
            {
                return true;
            }
        }
        

        private void ExecuteOperation(string element, Stack<string> stack)
        {
            var operand1 = stack.Pop();
            var operand2 = stack.Pop();

            switch (element)
            {
                case "+":
                    stack.Push((Int32.Parse(operand1) + Int32.Parse(operand2)).ToString());
                    break;
                case "*":
                    stack.Push((Int32.Parse(operand1) * Int32.Parse(operand2)).ToString());
                    break;
            }
        }

        private static bool IsOperation(string element)
        {
            return (element == "+") || (element == "*");
        }
    }
}
