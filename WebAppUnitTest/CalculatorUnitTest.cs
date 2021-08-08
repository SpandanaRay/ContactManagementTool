using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAppUnitTest
{
    public class Calculator
    {
        public int Sum(int num1, int num2)
        {
            return num1 + num2;
        }
    }

    [TestClass]
    public class CalculatorUnitTest
    {
        //A: Arrangement
        Calculator calc;
        public CalculatorUnitTest()
        {
            calc = new Calculator();
        }

        [TestMethod]
        public void TestAdd()
        {
            int num1 = 3, num2 = 5;

            //A: Action
            int result = calc.Sum(num1, num2);

            //A: Assertion
            Assert.AreEqual(num1 + num2, result);
        }
    }

}
