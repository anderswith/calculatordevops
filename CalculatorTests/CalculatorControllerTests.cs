using DevOpsCalculator.BE;
using DevOpsCalculator.BLL;
using DevOpsCalculator.BLL.Interfaces;
using DevOpsCalculator.Controllers;
using DevOpsCalculator.DAL.Repositories.interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace DevOpsCalculator.tests
{
    [TestFixture]
    public class CalculatorControllerTests
    {
        private Mock<ICalculator> _mockCalculator;
        private Mock<ICachedCalculator> _mockCachedCalculator;
        private Mock<ICalculatorRepository> _mockCalculatorRepository;
        private CalculatorController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockCalculator = new Mock<ICalculator>();
            _mockCachedCalculator = new Mock<ICachedCalculator>();
            _mockCalculatorRepository = new Mock<ICalculatorRepository>();
            _controller = new CalculatorController(_mockCalculator.Object, _mockCalculatorRepository.Object,
                _mockCachedCalculator.Object);
        }


        [Test]
        public void GetCachedResult_WhenResultNotFound_ReturnsNotFound()
        {
            _mockCachedCalculator.Setup(x => x.GetCachedResult<int>(1, 2, "+"))
                .Returns((CachedCalculator.Calculation<int>)null);

            var result = _controller.GetCachedResult(1, 2, "+");
            var notFoundResult = result.Result as NotFoundObjectResult;

            Assert.NotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public void Calculations_ReturnsOkWithData()
        {
            var calculations = new List<Calculation>
            {
                new Calculation { CalcString = "1 + 1 = 2" }
            };

            _mockCalculatorRepository.Setup(x => x.GetCalculations()).Returns(calculations);

            var result = _controller.Calculations() as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(calculations, result.Value);
        }


        [Test]
        public void CalculateAdd_ReturnsCorrectResult()
        {
            _mockCalculator.Setup(x => x.Add(1, 2)).Returns(3);

            var result = _controller.CalculateAdd(1, 2) as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(3, result.Value);
        }

        [Test]
        public void CalculateSubtract_ReturnsCorrectResult()
        {
            _mockCalculator.Setup(x => x.Subtract(5, 3)).Returns(2);

            var result = _controller.CalculateSubtract(5, 3) as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(2, result.Value);
        }

        [Test]
        public void CalculateMultiply_ReturnsCorrectResult()
        {
            _mockCalculator.Setup(x => x.Multiply(2, 3)).Returns(6);

            var result = _controller.CalculateMultiply(2, 3) as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(6, result.Value);
        }

        [Test]
        public void CalculateDivide_ReturnsCorrectResult()
        {
            _mockCalculator.Setup(x => x.Divide(6, 2)).Returns(3);

            var result = _controller.CalculateDivide(6, 2) as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(3, result.Value);
        }

        [Test]
        public void CalculateFactorial_ReturnsCorrectResult()
        {
            _mockCalculator.Setup(x => x.Factorial(5)).Returns(120);

            var result = _controller.CalculateFactorial(5) as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(120, result.Value);
        }

        [Test]
        public void CalculateIsPrime_ReturnsCorrectResult()
        {
            _mockCalculator.Setup(x => x.IsPrime(7)).Returns(true);

            var result = _controller.CalculateIsPrime(7) as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(true, result.Value);
        }
    }
}