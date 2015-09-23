using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interpretator;

namespace InterpretatorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void InterpretatorExpressionTest()
        {
            IInterpretator interpretator = new InterpretatorExpression();
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
        }
    }
}
