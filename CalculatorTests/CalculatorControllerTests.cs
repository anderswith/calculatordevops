using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using DevOpsCalculator.Controllers;
using DevOpsCalculator.BLL.Interfaces;
using DevOpsCalculator.DAL.Repositories.interfaces;
using DevOpsCalculator.BE;
using DevOpsCalculator.BLL;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DevOpsCalculator.Tests
{
    [TestFixture]
    public class CalculatorControllerTests
    {
        private Mock<ICalculator> _mockCalculator;
        private Mock<ICalculatorRepository> _mockCalculatorRepository;
        private Mock<ICachedCalculator> _mockCachedCalculator;
        private CalculatorController _controller;

        [SetUp]
        public void Setup()
        {
            _mockCalculator = new Mock<ICalculator>();
            _mockCalculatorRepository = new Mock<ICalculatorRepository>();
            _mockCachedCalculator = new Mock<ICachedCalculator>();
            _controller = new CalculatorController(_mockCalculator.Object, _mockCalculatorRepository.Object, _mockCachedCalculator.Object);
        }

        [Test]
        public void GetCachedResult_ReturnsNotFound_WhenCachedResultIsNull()
        {
            // Arrange: Mock cached result to return null
            _mockCachedCalculator.Setup(c => c.GetCachedResult<int>(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<string>()))
                .Returns((CachedCalculator.Calculation<int>)null);

            // Act
            var result = _controller.GetCachedResult(1, 2, "add");

            // Assert
            var actionResult = result.Result as NotFoundObjectResult;
            Assert.AreEqual("No cached result found.", actionResult.Value);
        }

        [Test]
        public void GetCachedResult_ReturnsOk_WhenCachedResultIsFound()
        {
            // Arrange: Mock a valid cached result
            var cachedResult = new CachedCalculator.Calculation<int>(1, "add", 3, 2);
            _mockCachedCalculator.Setup(c => c.GetCachedResult<int>(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<string>()))
                .Returns(cachedResult);

            // Act
            var result = _controller.GetCachedResult(1, 2, "add");

            // Assert
            var actionResult = result.Result as OkObjectResult;
            Assert.AreEqual(cachedResult, actionResult.Value);
        }

        [Test]
        public void Calculations_ReturnsOk_WhenCalculationsAreFound()
        {
            // Arrange: Mock calculator repository to return some calculations
            var calculations = new List<Calculation> { new Calculation { A = 1, B = 2, Result = 3 } };
            _mockCalculatorRepository.Setup(r => r.GetCalculations())
                .Returns(calculations);

            // Act
            var result = _controller.Calculations();

            // Assert
            var actionResult = result as OkObjectResult;
            Assert.AreEqual(calculations, actionResult.Value);
        }

        [Test]
        public void CalculateAdd_ReturnsOk_WhenModelIsValid()
        {
            // Arrange: Mock calculator to return a valid result
            var input = new CalculationInput { A = 3, B = 2 };
            _mockCalculator.Setup(c => c.Add(input.A, input.B)).Returns(5);

            // Act
            var result = _controller.CalculateAdd(input);

            // Assert
            var actionResult = result as OkObjectResult;
            Assert.AreEqual(5, actionResult.Value);
        }

        [Test]
        public void CalculateAdd_ReturnsOkResult_WhenModelIsValid()
        {
            // Arrange: Create a valid CalculationInput model
            var input = new CalculationInput { A = 5, B = 10 };

            // Mock the calculator to return a specific result
            _mockCalculator.Setup(c => c.Add(input.A, input.B)).Returns(15);

            // Act: Call the CalculateAdd method with the valid model
            var result = _controller.CalculateAdd(input);

            // Assert: Check that the result is an OkObjectResult
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "Expected OkObjectResult, but the result is null.");

            // Assert: Check that the result value is 15 (the expected sum)
            Assert.AreEqual(15, okResult?.Value);
        }
        

        [Test]
        public void CalculateSubtract_ReturnsOk_WhenModelIsValid()
        {
            // Arrange: Mock calculator to return a valid result
            var input = new CalculationInput { A = 3, B = 2 };
            _mockCalculator.Setup(c => c.Subtract(input.A, input.B)).Returns(1);

            // Act
            var result = _controller.CalculateSubtract(input);

            // Assert
            var actionResult = result as OkObjectResult;
            Assert.AreEqual(1, actionResult.Value);
        }

        [Test]
        public void CalculateMultiply_ReturnsOk_WhenModelIsValid()
        {
            // Arrange: Mock calculator to return a valid result
            var input = new CalculationInput { A = 3, B = 2 };
            _mockCalculator.Setup(c => c.Multiply(input.A, input.B)).Returns(6);

            // Act
            var result = _controller.CalculateMultiply(input);

            // Assert
            var actionResult = result as OkObjectResult;
            Assert.AreEqual(6, actionResult.Value);
        }

        [Test]
        public void CalculateDivide_ReturnsOk_WhenModelIsValid()
        {
            // Arrange: Mock calculator to return a valid result
            var input = new CalculationInput { A = 6, B = 2 };
            _mockCalculator.Setup(c => c.Divide(input.A, input.B)).Returns(3);

            // Act
            var result = _controller.CalculateDivide(input);

            // Assert
            var actionResult = result as OkObjectResult;
            Assert.AreEqual(3, actionResult.Value);
        }

        [Test]
        public void CalculateFactorial_ReturnsOk_WhenModelIsValid()
        {
            // Arrange: Mock calculator to return a valid result
            var input = new FactorialInput { N = 5 };
            _mockCalculator.Setup(c => c.Factorial(input.N)).Returns(120);

            // Act
            var result = _controller.CalculateFactorial(input);

            // Assert
            var actionResult = result as OkObjectResult;
            Assert.AreEqual(120, actionResult.Value);
        }

        [Test]
        public void CalculateIsPrime_ReturnsOk_WhenModelIsValid()
        {
            // Arrange: Mock the calculator to return a valid result
            var input = new PrimeCheckInput { Candidate = 5 };
            _mockCalculator.Setup(c => c.IsPrime(input.Candidate)).Returns(true);

            // Act
            var result = _controller.CalculateIsPrime(input);

            // Assert
            var actionResult = result as OkObjectResult;

            // Assert that the result is an OkObjectResult
            Assert.IsNotNull(actionResult, "Expected OkObjectResult, but the result is null.");
            
        }

    }
}

