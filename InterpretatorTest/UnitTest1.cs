using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interpretator;
using Interpretator.Interpretator;
using Interpretator.Interpretator.Type.Array;

namespace InterpretatorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void BoolExpTest()
        {
            var context = new Context();
            var interpretator = new BoolInterpretator(context);

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
            var interpretator = new BoolInterpretator(context);

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
            var interpretator = new BoolInterpretator(context);

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
            var interpretator = new BoolInterpretator(context);

            var expressionString = "5 >= 4 && 5*4 != 21 && (\"Apple\" == \"Apple\")";
            var result = interpretator.Run(expressionString);
            Assert.AreEqual(5 >= 4 && 5*4 != 21 && ("Apple" == "Apple"), result);

            expressionString = "(5-3) == (12-10)";
            result = interpretator.Run(expressionString);
            Assert.AreEqual((5 - 3) == (12 - 10), result);
        }

        [TestMethod]
        public void InitVariableTest()
        {
            var context = new Context();
            var interpretator = new VariableInterpretator(context);

            var expressionString = "int a = 54-33;";
            interpretator.Run(expressionString);
            Assert.AreEqual(54 - 33, (int)context.Lookup("a").Value);

            expressionString = "real b = 154.26/4.12+15*(15.12*45.87)-151;";
            interpretator.Run(expressionString);
            Assert.AreEqual(154.26 / 4.12 + 15 * (15.12 * 45.87) - 151, (double)context.Lookup("b").Value);

            expressionString = "bool c = (5 >= 4) && (5*4 != 21) && (\"Apple\" == \"Apple\");";
            interpretator.Run(expressionString);
            Assert.AreEqual((5 >= 4) && (5*4 != 21) && ("Apple" == "Apple"), (bool)context.Lookup("c").Value);

            expressionString = "string s = \"Pinkie Pie\" + \" is coolest pony\";";
            interpretator.Run(expressionString);
            Assert.AreEqual("Pinkie Pie is coolest pony", (string)context.Lookup("s").Value);
        }

        [TestMethod]
        public void MultiInitVariableTest()
        {
            var context = new Context();
            var interpretatorRegion = new RegionInterpretator(context);

            var expressionString = "int a = 54-33;" +
                                   "real b = 154.26/4.12+15*(15.12*45.87)-151;" +
                                   "bool c = (5 >= 4) && (5*4 != 21) && (\"Apple\" == \"Apple\");"+
                                   "string s = \"Pinkie Pie\" + \" is coolest pony\";";

            interpretatorRegion.Run(expressionString);
            Assert.AreEqual(54 - 33, (int)context.Lookup("a").Value);
            Assert.AreEqual(154.26 / 4.12 + 15 * (15.12 * 45.87) - 151, (double)context.Lookup("b").Value);
            Assert.AreEqual((5 >= 4) && (5 * 4 != 21) && ("Apple" == "Apple"), (bool)context.Lookup("c").Value);
            Assert.AreEqual("Pinkie Pie is coolest pony", (string)context.Lookup("s").Value);
        }

        [TestMethod]
        public void UseVariableInExpressionTest()
        {
            var context = new Context();
            var interpretatorRegion = new RegionInterpretator(context);


            var expressionString = "int a = 54-33;" +
                                   "real b = 154.26/4.12+15*a-151;" +
                                   "bool c = (b >= 4) && (a == 21) && (\"Apple\" == \"Apple\");" +
                                   "string pinkie = \"Pinkie Pie\";" +
                                   "string s = pinkie + \" is coolest pony\";";

            interpretatorRegion.Run(expressionString);
            Assert.AreEqual(54 - 33, (int)context.Lookup("a").Value);
            Assert.AreEqual(154.26 / 4.12 + 15 * (54 - 33) - 151, (double)context.Lookup("b").Value);
            Assert.AreEqual((154.26 / 4.12 + 15 * (54 - 33) - 151 >= 4) && (54 - 33 == 21) && ("Apple" == "Apple"), (bool)context.Lookup("c").Value);
            Assert.AreEqual("Pinkie Pie is coolest pony", (string)context.Lookup("s").Value);
        }

        [TestMethod]
        public void AccessVariableAfterInit()
        {
            var context = new Context();
            var interpretatorRegion = new RegionInterpretator(context);


            var expressionString = "int a = 0;" +
                                   "a = 54-33;" +
                                   "real b = 0;" +
                                   "b = 154.26/4.12+15*a-151;" +
                                   "bool c = false;" +
                                   "c = (b >= 4) && (a == 21) && (\"Apple\" == \"Apple\");" +
                                   "string pinkie = \"\";" +
                                   "pinkie =  \"Pinkie Pie\";" +
                                   "string s = pinkie + \" is coolest pony\";";
            interpretatorRegion.Run(expressionString);
            Assert.AreEqual(54 - 33, (int)context.Lookup("a").Value);
            Assert.AreEqual(154.26 / 4.12 + 15 * (54 - 33) - 151, (double)context.Lookup("b").Value);
            Assert.AreEqual((154.26 / 4.12 + 15 * (54 - 33) - 151 >= 4) && (54 - 33 == 21) && ("Apple" == "Apple"), (bool)context.Lookup("c").Value);
            Assert.AreEqual("Pinkie Pie is coolest pony", (string)context.Lookup("s").Value);
        }

        [TestMethod]
        public void IntArrayInitTest()
        {
            var context = new Context();
            var interpretator = new VariableInterpretator(context);

            var expressionString = "int[] a = new int[2];";
            interpretator.Run(expressionString);
            Assert.AreEqual(0, (context.Lookup("a").Value as int[])[0]);
            Assert.AreEqual(0, (context.Lookup("a").Value as int[])[1]);
            try
            {
                var errorVariable = (context.Lookup("a").Value as int[])[2];
                Assert.Fail();
            }
            catch (IndexOutOfRangeException ex)
            {
            }
        }

        [TestMethod]
        public void RealArrayInitTest()
        {
            var context = new Context();
            var interpretator = new VariableInterpretator(context);

            var expressionString = "real[] a = new real[2];";
            interpretator.Run(expressionString);
            Assert.AreEqual(0, (context.Lookup("a").Value as double[])[0]);
            Assert.AreEqual(0, (context.Lookup("a").Value as double[])[1]);
            try
            {
                var errorVariable = (context.Lookup("a").Value as double[])[2];
                Assert.Fail();
            }
            catch (IndexOutOfRangeException ex)
            {
            }
        }

        [TestMethod]
        public void BoolArrayInitTest()
        {
            var context = new Context();
            var interpretator = new VariableInterpretator(context);

            var expressionString = "bool[] a = new bool[2];";
            interpretator.Run(expressionString);
            Assert.AreEqual(false, (context.Lookup("a").Value as bool[])[0]);
            Assert.AreEqual(false, (context.Lookup("a").Value as bool[])[1]);
            try
            {
                var errorVariable = (context.Lookup("a").Value as bool[])[2];
                Assert.Fail();
            }
            catch (IndexOutOfRangeException ex)
            {
            }
        }

        [TestMethod]
        public void StringArrayInitTest()
        {
            var context = new Context();
            var interpretator = new VariableInterpretator(context);

            var expressionString = "string[] a = new string[2];";
            interpretator.Run(expressionString);
            Assert.AreEqual(null, (context.Lookup("a").Value as string[])[0]);
            Assert.AreEqual(null, (context.Lookup("a").Value as string[])[1]);
            try
            {
                var errorVariable = (context.Lookup("a").Value as string[])[2];
                Assert.Fail();
            }
            catch (IndexOutOfRangeException ex)
            {
            }
        }

        [TestMethod]
        public void IndexexIntArrayTest()
        {
            //var context = new Context();
            //var interpretatorRegion = new RegionInterpretator(context);

            //var expressionString = "int[] a = new int[1];" +
            //                       "a[0] = 2;";
            //interpretatorRegion.Run(expressionString);
            //Assert.AreEqual(2, (context.Lookup("a").Value as int[])[0]);
        }
    }
}
