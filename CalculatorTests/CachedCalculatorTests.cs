using Moq;
using DevOpsCalculator.BLL;
using DevOpsCalculator.DAL.Repositories.interfaces;

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
            int result = _cachedCalculator.Add(2, 3);
            Assert.AreEqual(5, result);
        }

        [Test]
        public void Subtract_ShouldReturnCorrectResult()
        {
            int result = _cachedCalculator.Subtract(5, 3);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void Multiply_ShouldReturnCorrectResult()
        {
            int result = _cachedCalculator.Multiply(4, 3);
            Assert.AreEqual(12, result);
        }

        [Test]
        public void Divide_ShouldReturnCorrectResult()
        {
            int result = _cachedCalculator.Divide(10, 2);
            Assert.AreEqual(5, result);
        }

        [Test]
        public void Factorial_ShouldReturnCorrectResult()
        {
            int result = _cachedCalculator.Factorial(5);
            Assert.AreEqual(120, result);
        }

        [Test]
        public void IsPrime_ShouldReturnTrueForPrimeNumbers()
        {
            bool result = _cachedCalculator.IsPrime(7);
            Assert.IsTrue(result);
        }

        [Test]
        public void IsPrime_ShouldReturnFalseForNonPrimeNumbers()
        {
            bool result = _cachedCalculator.IsPrime(10);
            Assert.IsFalse(result);
        }

        [Test]
        public void GetCachedResult_ShouldReturnCachedValue()
        {
            _cachedCalculator.Add(2, 3); // First calculation should store in cache
            var cachedResult = _cachedCalculator.GetCachedResult<int>(2, 3, "Add");
            Assert.IsNotNull(cachedResult);
            Assert.AreEqual(5, cachedResult.Result);
        }
    }
}