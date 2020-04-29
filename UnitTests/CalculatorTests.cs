using LineCalculator;
using NUnit.Framework;

namespace UnitTests
{
    public class CalculatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCalculate()
        {
            // //arrange
            // Calculator calculator = new Calculator();
            // string line = "25.6 * 5 + 4 / 5 - 3 ";
            // float expected = 25.6f * 5 + 4 / 5 - 3;
            // float result;
            
            //By some reason C# calculates the line wrong
            
            //arrange
            Calculator calculator = new Calculator();
            string line = "45 / 3 * 85 - 4";
            float expected = 45 / 3 * 85 - 4;
            float result;
            
            //act
            result = calculator.Calculate(line).value;
            
            //assert
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void TestCalculateBrackets()
        {
            
            //arrange
            Calculator calculator = new Calculator();
            string line = "2 * (3 + 4)";
            float expected = 2 * (3 + 4);
            float result;
            
            //act
            result = calculator.Calculate(line).value;
            
            //assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestParseNumber()
        {
            //arrange
            Calculator calculator = new Calculator();
            string num = "22.5";
            float result;

            //act
            result = calculator.ParseNumber(num);
            
            //assert
            Assert.AreEqual(22.5f, result);
        }

        [Test]
        public void TestPlusCalculateTwoLastNumbers()
        {
            //arrange
            Calculator calculator = new Calculator();
            calculator.Numbers.Push(10);
            calculator.Numbers.Push(13.5f);
            float result;
            
            //act
            result = calculator.CalculateTwoLastNumbers('+');
            
            //assert
            Assert.AreEqual(10 + 13.5f, result);
        }

        [Test]
        public void TestMinusCalculateTwoLastNumbers()
        {
            //arrange
            Calculator calculator = new Calculator();
            calculator.Numbers.Push(10);
            calculator.Numbers.Push(13.5f);
            float result;
            
            //act
            result = calculator.CalculateTwoLastNumbers('-');
            
            //assert
            Assert.AreEqual(10 - 13.5f, result);
        }

        [Test]
        public void TestMultiplyCalculateTwoLastNumbers()
        {
            //arrange
            Calculator calculator = new Calculator();
            calculator.Numbers.Push(10);
            calculator.Numbers.Push(13.5f);
            float result;
            
            //act
            result = calculator.CalculateTwoLastNumbers('*');
            
            //assert
            Assert.AreEqual(10 * 13.5f, result);
        }

        [Test]
        public void TestDivideCalculateTwoLastNumbers()
        {
            //arrange
            Calculator calculator = new Calculator();
            calculator.Numbers.Push(10);
            calculator.Numbers.Push(13.5f);
            float result;
            
            //act
            result = calculator.CalculateTwoLastNumbers('/');
            
            //assert
            Assert.AreEqual(10 / 13.5f, result);
        }
    }
}