using DevOpsCalculator.BLL;
using DevOpsCalculator.BLL.Interfaces;
using DevOpsCalculator.DAL.Repositories.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevOpsCalculator.Controllers;

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
    
    [HttpGet("[action]")]
    public ActionResult<CachedCalculator.Calculation<int>> GetCachedResult(int a, int? b, string operation)
    {
        var cachedResult = _cachedCalculator.GetCachedResult<int>(a, b, operation);

        if (cachedResult == null)
        {
            return NotFound("No cached result found.");
        }

        return Ok(cachedResult);
    }
    [HttpGet("[action]")]
    public IActionResult Calculations()
    {
        var calculations = _calculatorRepository.GetCalculations();
        return Ok(calculations);
    }

    [HttpPost("[action]")]
    public IActionResult CalculateAdd(int a, int b)
    {
        var result = _calculator.Add(a, b);
        return Ok(result);
    }
    [HttpPost("[action]")]
    public IActionResult CalculateSubtract(int a, int b)
    {
        var result = _calculator.Subtract(a, b);
        return Ok(result);
    }

    [HttpPost("[action]")]
    public IActionResult CalculateMultiply(int a, int b)
    {
        var result = _calculator.Multiply(a, b);
        return Ok(result);
    }

    [HttpPost("[action]")]
    public IActionResult CalculateDivide(int a, int b)
    {
        var result = _calculator.Divide(a, b);
        return Ok(result);
    }

    [HttpPost("[action]")]
    public IActionResult CalculateFactorial(int n)
    {
        var result = _calculator.Factorial(n);
        return Ok(result);
    }

    [HttpPost("[action]")]
    public IActionResult CalculateIsPrime(int candidate)
    {
        var result = _calculator.IsPrime(candidate);
        return Ok(result);
    }
    
}