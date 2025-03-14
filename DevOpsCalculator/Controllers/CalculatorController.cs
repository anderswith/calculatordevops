using DevOpsCalculator.BE;
using DevOpsCalculator.BLL;
using DevOpsCalculator.BLL.Interfaces;
using DevOpsCalculator.DAL.Repositories.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevOpsCalculator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly ICalculator _calculator;
    private readonly ICachedCalculator _cachedCalculator;
    private readonly ICalculatorRepository _calculatorRepository;

    public CalculatorController(ICalculator calculator, ICalculatorRepository calculatorRepository, ICachedCalculator cachedCalculator)
    {
        _calculator = calculator;
        _calculatorRepository = calculatorRepository;
        _cachedCalculator = cachedCalculator;
    }
    
    [HttpGet("GetCachedResult")]
    public ActionResult<CachedCalculator.Calculation<int>> GetCachedResult(int a, int? b, string operation)
    {
        if (string.IsNullOrEmpty(operation))
        {
            ModelState.AddModelError("operation", "Operation is required.");
            return BadRequest(ModelState); // Return BadRequest with ModelState errors
        }

        Console.WriteLine($"Received request for GetCachedResult with a={a}, b={b}, operation={operation}");

        var cachedResult = _cachedCalculator.GetCachedResult<int>(a, b, operation);
        if (cachedResult == null)
        {
            Console.WriteLine($"No cached result found for key.");
            return NotFound("No cached result found.");
        }

        Console.WriteLine($"Found cached result for key.");
        return Ok(cachedResult);
    }

    [ProducesResponseType(typeof(List<>), 200)]  // 200 OK, returning a list
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]  // 400 BadRequest, returning ModelState errors
    [HttpGet("[action]")]
    public IActionResult Calculations()
    {

        var calculations = _calculatorRepository.GetCalculations();
        return Ok(calculations);
    }

    // POST method for adding numbers
    [ProducesResponseType(typeof(int), 200)]  // 200 OK, returning an integer
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]  // 400 BadRequest, returning ModelState errors
    [HttpPost("[action]")]
    public IActionResult CalculateAdd([FromBody] CalculationInput input)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = _calculator.Add(input.A, input.B);
        return Ok(result);
    }

    // POST method for subtracting numbers
    [ProducesResponseType(typeof(int), 200)]  // 200 OK, returning an integer
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]  // 400 BadRequest, returning ModelState errors
    [HttpPost("[action]")]
    public IActionResult CalculateSubtract([FromBody] CalculationInput input)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = _calculator.Subtract(input.A, input.B);
        return Ok(result);
    }

    // POST method for multiplying numbers
    [ProducesResponseType(typeof(int), 200)]  // 200 OK, returning an integer
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]  // 400 BadRequest, returning ModelState errors
    [HttpPost("[action]")]
    public IActionResult CalculateMultiply([FromBody] CalculationInput input)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = _calculator.Multiply(input.A, input.B);
        return Ok(result);
    }

    // POST method for dividing numbers
    [ProducesResponseType(typeof(int), 200)]  // 200 OK, returning an integer
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]  // 400 BadRequest, returning ModelState errors
    [HttpPost("[action]")]
    public IActionResult CalculateDivide([FromBody] CalculationInput input)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = _calculator.Divide(input.A, input.B);
        return Ok(result);
    }

    // POST method for calculating factorial
    [ProducesResponseType(typeof(int), 200)]  // 200 OK, returning an integer
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]  // 400 BadRequest, returning ModelState errors
    [HttpPost("[action]")]
    public IActionResult CalculateFactorial([FromBody] FactorialInput input)
    {
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = _calculator.Factorial(input.N);
        return Ok(result);
    }

    // POST method for checking prime number
    [ProducesResponseType(typeof(bool), 200)]  // 200 OK, returning a boolean
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]  // 400 BadRequest, returning ModelState errors
    [HttpPost("[action]")]
    public IActionResult CalculateIsPrime([FromBody] PrimeCheckInput input)
    {
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = _calculator.IsPrime(input.Candidate);
        return Ok(result);
    }
    
}