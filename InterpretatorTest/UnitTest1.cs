using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interpretator;
using Interpretator.Interpretator;

namespace InterpretatorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void BoolExpTest()
        {
            var context = new Context();
            var interpretator = new BooleanInterpretator(context);

            var expressionString = "true && false";
            var result = interpretator.Run(expressionString);
            Assert.AreEqual(false, result);

            expressionString = "true && false || true";
            result = interpretator.Run(expressionString);
            Assert.AreEqual(true, result);

            expressionString = "true && (false || true)";
            result = interpretator.Run(expressionString);
            Assert.AreEqual(true, result);

            expressionString = "true && false || true && (!true || false)";
            result = interpretator.Run(expressionString);
            Assert.AreEqual(false, result);

            expressionString = "true && (!false && (true || !false && true)) || false";
            result = interpretator.Run(expressionString);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IntExpTest()
        {
            var context = new Context();
            var interpretator = new IntInterpretator(context);

            var expressionSting = "14+35";
            var result = interpretator.Run(expressionSting);
            Assert.AreEqual(14 + 35, result);

            expressionSting = "124-182";
            result = interpretator.Run(expressionSting);
            Assert.AreEqual(124-182, result);

            expressionSting = "12*15";
            result = interpretator.Run(expressionSting);
            Assert.AreEqual(12 * 15, result);

            expressionSting = "1579/17";
            result = interpretator.Run(expressionSting);
            Assert.AreEqual(1579 / 17, result);

            expressionSting = "12574/45*15";
            result = interpretator.Run(expressionSting);
            Assert.AreEqual(12574 / 45 * 15, result);

            expressionSting = "(5/284+3)*45+234/(42-12*5*(5-2))";
            result = interpretator.Run(expressionSting);
            Assert.AreEqual((5/284+3)*45+234/(42-12*5*(5-2)), result);
        }

        [TestMethod]
        public void RealExpTest()
        {
            var context = new Context();
            var interpretator = new RealInterpretator(context);
            
            var expressionSting = "14.45+0.167";
            var result = interpretator.Run(expressionSting);
            Assert.AreEqual(14.45 + 0.167, result, 0.001);

            expressionSting = "124.31-48.157";
            result = interpretator.Run(expressionSting);
            Assert.AreEqual(124.31 - 48.157, result, 0.001);

            expressionSting = "127.45*0.1257";
            result = interpretator.Run(expressionSting);
            Assert.AreEqual(127.45 * 0.1257, result, 0.001);

            expressionSting = "1584.2251/48.5987";
            result = interpretator.Run(expressionSting);
            Assert.AreEqual(1584.2251 / 48.5987, result, 0.001);

            expressionSting = "(58.25/4.21*59.23+6.1-48.26)/6.26+458.157/45.2655*5988.212*(124.157+52.26*(124.264+263.33*48.65)+1.15-58.55)";
            result = interpretator.Run(expressionSting);
            Assert.AreEqual((58.25 / 4.21 * 59.23 + 6.1 - 48.26) / 6.26 + 458.157 / 45.2655 * 5988.212 * (124.157 + 52.26 * (124.264 + 263.33 * 48.65) + 1.15 - 58.55), result, 0.001);
        }

        [TestMethod]
        public void StringExpTest()
        {
            var context = new Context();
            var interpretator = new StringInterpretator(context);

            var expressionString = "\"Pinkie\"+\" Pie\"";
            var result = interpretator.Run(expressionString);
            Assert.AreEqual("Pinkie Pie", result);
        }

        [TestMethod]
        public void RealIntTest()
        {
            var context = new Context();
            var interpretator = new RealInterpretator(context);

            var expressionSting = "14.45+142";
            var result = interpretator.Run(expressionSting);
            Assert.AreEqual(14.45 + 142, result, 0.001);

            expressionSting = "124-48.157";
            result = interpretator.Run(expressionSting);
            Assert.AreEqual(124 - 48.157, result, 0.001);

            expressionSting = "127*0.1257";
            result = interpretator.Run(expressionSting);
            Assert.AreEqual(127 * 0.1257, result, 0.001);

            expressionSting = "1584.2251/48";
            result = interpretator.Run(expressionSting);
            Assert.AreEqual(1584.2251 / 48, result, 0.001);

            expressionSting = "(58.25/41*59.23+6-48.26)/6+458.157/45.2655*59882*(124.157+52*(124.264+263*48.65)+1.15-58)";
            result = interpretator.Run(expressionSting);
            Assert.AreEqual((58.25 / 41 * 59.23 + 6 - 48.26) / 6 + 458.157 / 45.2655 * 59882 * (124.157 + 52 * (124.264 + 263 * 48.65) + 1.15 - 58), result, 0.001);
        }

        [TestMethod]
        public void StringBooleanTest()
        {
            var context = new Context();
            var interpretator = new BooleanInterpretator(context);

            var expressionString = "\"Rainbow\" == \"Rainbow\"";
            var result = interpretator.Run(expressionString);
            Assert.AreEqual(true, result);

            expressionString = "\"Rainbow\" != \"Rainbow\"";
            result = interpretator.Run(expressionString);
            Assert.AreEqual(false, result);

            expressionString = "\"Rainbow\" != \"Rainbow\" || true";
            result = interpretator.Run(expressionString);
            Assert.AreEqual(true, result);

            expressionString = "\"Rainbow\" == \"Dash\" && (\"Apple\" != \"Jack=\" || false)";
            result = interpretator.Run(expressionString);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IntBooleanTest()
        {
            var context = new Context();
            var interpretator = new BooleanInterpretator(context);

            var expressionString = "5 == 4";
            var result = interpretator.Run(expressionString);
            Assert.AreEqual(false, result);

            expressionString = "5.56 != 4";
            result = interpretator.Run(expressionString);
            Assert.AreEqual(true, result);

            expressionString = "5.56 > 4";
            result = interpretator.Run(expressionString);
            Assert.AreEqual(true, result);

            expressionString = "5.56 >= 5.56";
            result = interpretator.Run(expressionString);
            Assert.AreEqual(true, result);

            expressionString = "5.56 <= 5.56";
            result = interpretator.Run(expressionString);
            Assert.AreEqual(true, result);

            expressionString = "5.56 < 5.56";
            result = interpretator.Run(expressionString);
            Assert.AreEqual(false, result);

            expressionString = "5.56 <= 5.56 && 5 >= 4 || 34.21 == 54.23";
            result = interpretator.Run(expressionString);
            Assert.AreEqual(5.56 <= 5.56 && 5 >= 4 || 34.21 == 54.23, result);
        }

        [TestMethod]
        public void BooleanHardoceTest()
        {
            var context = new Context();
            var interpretator = new BooleanInterpretator(context);

            var expressionString = "5 >= 4 && 5*4 != 21 && (\"Apple\" == \"Apple\")";
            var result = interpretator.Run(expressionString);
            Assert.AreEqual(5 >= 4 && 5*4 != 21 && ("Apple" == "Apple"), result);
        }
    }
}
