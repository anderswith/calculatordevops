using System.Diagnostics.CodeAnalysis;
using Moq;
using DevOpsCalculator.BLL;
using DevOpsCalculator.DAL.Repositories.interfaces;
using NUnit.Framework;


namespace DevOpsCalculator.Tests
{
    [TestFixture]
    public class CachedCalculatorTests
    {
        private CachedCalculator _cachedCalculator;
        private Mock<ICalculatorRepository> _mockRepository;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<ICalculatorRepository>();
            _cachedCalculator = new CachedCalculator(_mockRepository.Object);
        }

        [Test]
        public void Add_ShouldReturnCorrectResult()
        {
            var expected = 5;
            int result = _cachedCalculator.Add(2, 3);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Subtract_ShouldReturnCorrectResult()
        {
            var expected = 2;
            int result = _cachedCalculator.Subtract(5, 3);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Multiply_ShouldReturnCorrectResult()
        {
            var expected = 12;
            int result = _cachedCalculator.Multiply(4, 3);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Divide_ShouldReturnCorrectResult()
        {
            var expected = 5;
            int result = _cachedCalculator.Divide(10, 2);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Factorial_ShouldReturnCorrectResult()
        {
            var expected = 120;
            int result = _cachedCalculator.Factorial(5);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void IsPrime_ShouldReturnTrueForPrimeNumbers()
        {

            bool result = _cachedCalculator.IsPrime(7);
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsPrime_ShouldReturnFalseForNonPrimeNumbers()
        {
            bool result = _cachedCalculator.IsPrime(10);
            Assert.That(result, Is.False);
        }

        [Test]
        public void GetCachedResult_ShouldReturnCachedValue()
        {
            var expected = 5;
            _cachedCalculator.Add(2, 3); // First calculation should store in cache
            var cachedResult = _cachedCalculator.GetCachedResult<int>(2, 3, "Add");
            Assert.IsNotNull(cachedResult);
            Assert.That(cachedResult.Result, Is.EqualTo(expected));
        }
    }
}