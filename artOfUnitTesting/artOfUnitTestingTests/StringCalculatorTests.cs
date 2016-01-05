using System;
using NUnit.Framework;
using artOfUnitTesting;

//http://osherove.com/tdd-kata-1
namespace artOfUnitTestingTests
{
    public class FakeSettings : ISettings
    {
        public bool IsEnabled { get; set; }
    }

    [TestFixture]
    public class StringCalculatorTests
    {

        private static StringCalculator StringCalculatorFactory(bool isEnabled = true)
        {
            var calculator = new StringCalculator(new FakeSettings { IsEnabled = isEnabled });
            return calculator;
        }

        [Test]
        public void Add_EmptyString_ReturnDefaultValue()
        {
            var calculator = StringCalculatorFactory();

            var sum = calculator.Add("");

            Assert.AreEqual(0, sum);
        }

        [Test]
        public void Add_IsEnabledFalse_ThrowsException()
        {
            var calculator = StringCalculatorFactory(false);

            Assert.Throws<ArgumentException>(() => calculator.Add(""));
        }

        [TestCase("1",1)]
        [TestCase("2",2)]
        public void Add_OneNumber_ReturnThatNumber(string number, int expectedSum)
        {
            var calculator = StringCalculatorFactory();

            var sum = calculator.Add(number);

            Assert.AreEqual(expectedSum, sum);
        }

        [TestCase("1,2", 3)]
        [TestCase("3,4", 7)]
        public void Add_TwoNumbers_ReturnSumOfThoseNumbers(string numbers, int expectedSum)
        {
            var calculator = StringCalculatorFactory();

            var sum = calculator.Add(numbers);

            Assert.AreEqual(expectedSum, sum);
        }
        
        [TestCase("1,2,3,4", 10)]
        [TestCase("1,2,3,5,7,11,13,17", 59)]
        [TestCase("1000,100000", 101000)]
        public void Add_MultipleNumbers_ReturnSumOfThoseNumbers(string numbers, int expectedSum)
        {
            var calculator = StringCalculatorFactory();

            var sum = calculator.Add(numbers);

            Assert.AreEqual(expectedSum, sum);
        }

        [TestCase("1\n2", 3)]
        [TestCase("1\n2\n3", 6)]
        [TestCase("1\n2,3", 6)]
        [TestCase("1,2\n3", 6)]
        public void Add_NumbersSeparatedByNewLineDelimiter_ReturnSumOfNumbers(string numbers, int expectedSum)
        {
            var calculator = StringCalculatorFactory();

            var sum = calculator.Add(numbers);

            Assert.AreEqual(expectedSum, sum);
        }

        [TestCase(@"//[$]\n2", 2)]
        [TestCase(@"//[$]\n2$3", 5)]
        [TestCase(@"//[$]\n2$3$9", 14)]
        [TestCase(@"//[$]\n2$3,9", 14)]
        [TestCase(@"//[$]\n2,3$9", 14)]
        public void Add_NumbersSeparatedByCustomDelimiter_ReturnSumOfNumbers(string numbers, int expectedSum)
        {
            var calculator = StringCalculatorFactory();

            var sum = calculator.Add(numbers);

            Assert.AreEqual(expectedSum, sum);
        }
        
        [TestCase(@"2,-3")]
        public void Add_NegativeNumbersNotAllowed_ThrowsException(string numbers)
        {
            var calculator = StringCalculatorFactory();

            try
            {
                var sum = calculator.Add(numbers);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Negatives not allowed");
            }
        }
    }
}
