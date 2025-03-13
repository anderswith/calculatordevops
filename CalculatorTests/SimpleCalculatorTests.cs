using NUnit.Framework;
using Moq;
using DevOpsCalculator.BLL;
using System;
using DevOpsCalculator.BE;
using DevOpsCalculator.DAL.Repositories.interfaces;

namespace DevOpsCalculator.Tests
{
    [TestFixture]
    public class SimpleCalculatorTests
    {
        private SimpleCalculator _simpleCalculator;
        private Mock<ICalculatorRepository> _mockRepository;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<ICalculatorRepository>();
            _simpleCalculator = new SimpleCalculator(_mockRepository.Object);
        }

        [Test]
        public void Add_ShouldReturnCorrectResultAndSaveToDb()
        {
            int result = _simpleCalculator.Add(2, 3);
            Assert.AreEqual(5, result);
            _mockRepository.Verify(r => r.AddCalculation(It.Is<Calculation>(c => c.CalcString == "2+3=5")), Times.Once);
        }

        [Test]
        public void Subtract_ShouldReturnCorrectResultAndSaveToDb()
        {
            int result = _simpleCalculator.Subtract(5, 3);
            Assert.AreEqual(2, result);
            _mockRepository.Verify(r => r.AddCalculation(It.Is<Calculation>(c => c.CalcString == "5-3=2")), Times.Once);
        }

        [Test]
        public void Multiply_ShouldReturnCorrectResultAndSaveToDb()
        {
            int result = _simpleCalculator.Multiply(4, 3);
            Assert.AreEqual(12, result);
            _mockRepository.Verify(r => r.AddCalculation(It.Is<Calculation>(c => c.CalcString == "4*3=12")), Times.Once);
        }

        [Test]
        public void Divide_ShouldReturnCorrectResultAndSaveToDb()
        {
            int result = _simpleCalculator.Divide(10, 2);
            Assert.AreEqual(5, result);
            _mockRepository.Verify(r => r.AddCalculation(It.Is<Calculation>(c => c.CalcString == "10/2=5")), Times.Once);
        }

        [Test]
        public void Divide_ByZero_ShouldThrowException()
        {
            Assert.Throws<DivideByZeroException>(() => _simpleCalculator.Divide(10, 0));
        }

        [Test]
        public void Factorial_ShouldReturnCorrectResultAndSaveToDb()
        {
            int result = _simpleCalculator.Factorial(5);
            Assert.AreEqual(120, result);
            _mockRepository.Verify(r => r.AddCalculation(It.Is<Calculation>(c => c.CalcString == "Factorial 5 = 120")), Times.Once);
        }

        [Test]
        public void Factorial_NegativeNumber_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _simpleCalculator.Factorial(-1));
        }

        [Test]
        public void Factorial_Zero_ShouldReturnOne()
        {
            var expected = 1;
            int result = _simpleCalculator.Factorial(0);
            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void IsPrime_ShouldReturnTrueForPrimeNumbersAndSaveToDb()
        {
            bool result = _simpleCalculator.IsPrime(7);
            Assert.IsTrue(result);
            _mockRepository.Verify(r => r.AddCalculation(It.Is<Calculation>(c => c.CalcString == "Is 7 a prime number True")), Times.Once);
        }

        [Test]
        public void IsPrime_ShouldReturnFalseForNonPrimeNumbersAndSaveToDb()
        {
            bool result = _simpleCalculator.IsPrime(10);
            Assert.IsFalse(result);
            _mockRepository.Verify(r => r.AddCalculation(It.Is<Calculation>(c => c.CalcString == "Is 10 a prime number False")), Times.Once);
        }

        [Test]
        public void IsPrime_LessThanTwo_ShouldReturnFalse()
        {
            
            bool result = _simpleCalculator.IsPrime(1);
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsPrime_PrimeEdgeCase_ShouldReturnTrue()
        {
            bool result = _simpleCalculator.IsPrime(2);
            Assert.That(result, Is.True);
        }
    }
}

