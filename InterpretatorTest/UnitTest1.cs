using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interpretator;
using Interpretator.Bool;

namespace InterpretatorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void InterpretatorExpressionTest()
        {
            var interpretator = new InterpretatorExpression();
            string expression = "4+5";
            var result = interpretator.Run(expression);
            int resultSumma = Convert.ToInt32(result.ToString());
            Assert.AreEqual(9, resultSumma);

            expression = "5*7";
            result = interpretator.Run(expression);
            resultSumma = Convert.ToInt32(result.ToString());
            Assert.AreEqual(35, resultSumma);
            

            expression = "4+5*7";
            result = interpretator.Run(expression);
            resultSumma = Convert.ToInt32(result.ToString());
            Assert.AreEqual(39, resultSumma);

            expression = "5*4*8";
            result = interpretator.Run(expression);
            resultSumma = Convert.ToInt32(result.ToString());
            Assert.AreEqual(160, resultSumma);

            expression = "4+5+5*3+5*8*9*7+1";
            result = interpretator.Run(expression);
            resultSumma = Convert.ToInt32(result.ToString());
            Assert.AreEqual(2545, resultSumma);

            expression = "(1+2)*3";
            result = interpretator.Run(expression);
            resultSumma = Convert.ToInt32(result.ToString());
            Assert.AreEqual(9, resultSumma);

            expression = "(12+231)*37";
            result = interpretator.Run(expression);
            resultSumma = Convert.ToInt32(result.ToString());
            Assert.AreEqual(8991, resultSumma);

            expression = "35*(140+45*3)+2";
            result = interpretator.Run(expression);
            resultSumma = Convert.ToInt32(result.ToString());
            Assert.AreEqual(9627, resultSumma);

            expression = "((35*(140+45*3)+2)*14+15+24)*3";
            result = interpretator.Run(expression);
            resultSumma = Convert.ToInt32(result.ToString());
            Assert.AreEqual(404451, resultSumma);

            expression = "5-4";
            result = interpretator.Run(expression);
            resultSumma = Convert.ToInt32(result.ToString());
            Assert.AreEqual(1, resultSumma);

            expression = "124-15-48-52-13";
            result = interpretator.Run(expression);
            resultSumma = Convert.ToInt32(result.ToString());
            Assert.AreEqual(-4, resultSumma);

            expression = "1237/15/3";
            result = interpretator.Run(expression);
            resultSumma = Convert.ToInt32(result.ToString());
            Assert.AreEqual(27, resultSumma);

            expression = "1237%145%45";
            result = interpretator.Run(expression);
            resultSumma = Convert.ToInt32(result.ToString());
            Assert.AreEqual(32, resultSumma);

            expression = "(123-45*23)+34%3+3/4+5*2+3*(45-32)/2-6*4/5";
            result = interpretator.Run(expression);
            resultSumma = Convert.ToInt32(result.ToString());
            Assert.AreEqual((123 - 45 * 23) + 34 % 3 + 3 / 4 + 5 * 2 + 3 * (45 - 32) / 2 - 6 * 4 / 5, resultSumma);
        }


        [TestMethod]
        public void BoolExpTest()
        {
            var context = new Context();
            var interpretator = new BooleanExpInterpretator(context);

            var expressionString = "true && false";
            var result = interpretator.Run(expressionString).Evaluate(context);
		    Assert.AreEqual(false, result);

            expressionString = "true && false || true";
            result = interpretator.Run(expressionString).Evaluate(context);
            Assert.AreEqual(true, result);

            expressionString = "true && (false || true)";
            result = interpretator.Run(expressionString).Evaluate(context);
            Assert.AreEqual(true, result);

            expressionString = "true && false || true && (!true || false)";
            result = interpretator.Run(expressionString).Evaluate(context);
            Assert.AreEqual(false, result);

            expressionString = "true && (!false && (true || !false && true)) || false";
            result = interpretator.Run(expressionString).Evaluate(context);
            Assert.AreEqual(true, result);
        }
    }
}
