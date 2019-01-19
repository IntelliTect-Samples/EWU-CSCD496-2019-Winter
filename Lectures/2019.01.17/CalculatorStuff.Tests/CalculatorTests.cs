using CalculatorStuff;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;

namespace CalculatorTests
{

    /**
     * 1. Write test that fails to show you need to write some code
     * 2. Write the simplest code to get test to pass.
     * 3. Refactor
     * 4. Go to #1
     * */

    [TestClass]
    public class CalculatorTests
    {
        public Calculator Calculator { get; set; }
        [TestInitialize]
        public void TestInitialize()
        {
            Calculator = new Calculator();
        }

        [TestMethod]
        public void Add_40Plus2_Return42()
        {
            Assert.AreEqual<int>(42, Calculator.Calculate("40+2"));
        }

        [TestMethod]
        public void Subtract_44Minus2_42()
        {
            Assert.AreEqual<int>(42, Calculator.Calculate("44-2"));
        }

        [TestMethod]
        [DataRow(43, "40+3")]
        [DataRow(42, "40+2")]
        [DataRow(0, "0+0")]
        [DataRow(1, "1-0")]
        public void Calculate_AddSubtractValidParameters_Calculate(
            int answer, string equation)
        {
            Assert.AreEqual<int>(
                answer, Calculator.Calculate(equation));
        }

        [TestMethod]
        public void Add_2Plus40_Return42()
        {
            Assert.AreEqual<int>(42, Calculator.Calculate("2+40"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_EquationIsNull_ThrowArgumentException()
        {
            Calculator.Calculate(null);
        }


        // I would tend to avoid Data driven tests that 
        // obfuscate the code.
        [TestMethod]
        [DataRow(typeof(ArgumentNullException), null)]
        [DataRow(typeof(ArgumentException), "40+")]
        [DataRow(typeof(ArgumentException), "40&5")]
        public void Calculate_InvalidEquations_ThrowArgumentException(
            Type exceptionType, string equation)
        {
            try
            {
                Calculator.Calculate(equation);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch(ArgumentException exception) when (
                exception.GetType() == exceptionType)
            {}
        }

        [TestMethod]
        [DataRow("40+2")]
        [DataRow("42-2")]
        [DataRow("42 +2")]
        [DataRow("42+ 2")]
        [DataRow(" 42+2")]
        [DataRow(" 42+2 ")]
        [DataRow(" 42 +2")]
        [DataRow(" 42-2")]
        public void EquationRegEx_ValidEquation_Success(
            string equation)
        {
            Match match = Calculator.EquationRegEx.Match(equation);
            Assert.IsTrue(match.Success);
        }

    }
}
