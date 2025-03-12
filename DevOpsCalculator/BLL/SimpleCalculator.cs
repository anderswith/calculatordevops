using DevOpsCalculator.BE;
using DevOpsCalculator.BLL.Interfaces;
using DevOpsCalculator.DAL.Repositories.interfaces;

namespace DevOpsCalculator.BLL;

public class SimpleCalculator : ICalculator
{
    private readonly ICalculatorRepository _repository;

    public SimpleCalculator(ICalculatorRepository repository)
    {
        _repository = repository;
    }
    public int Add(int a, int b)
    {
        var mathOperator = "+";
        var result = a + b;
        addSimpleMathToDb(a, b, mathOperator, result);
        return result;
    }

    public int Subtract(int a, int b)
    {
        var mathOperator = "-";
        var result = a - b;
        addSimpleMathToDb(a, b, mathOperator, result);
        return result;
    }

    public int Multiply(int a, int b)
    {
        var mathOperator = "*";
        var result = a * b;
        addSimpleMathToDb(a, b, mathOperator, result);
        return result;
    }

    public int Divide(int a, int b)
    {
        var mathOperator = "/";
        var result = a / b;
        addSimpleMathToDb(a, b, mathOperator, result);
        return result;
    }
    
    public int Factorial(int n)
    {
        if (n < 0)
        {
            throw new ArgumentException("Factorial is not defined for negative numbers");
        }
        if (n == 0)
        {
            return 1;
        }
        int result = ComputeFactorial(n); 
        addFactorialToDb(n, result);
        return result;
        
    }
    private int ComputeFactorial(int n)
    {
        if (n == 0) return 1;
        return n * ComputeFactorial(n - 1);
    }

    public void addFactorialToDb(int n, double result)
    {
        var calculation = new Calculation()
        {
            CalculationId = Guid.NewGuid(),
            CalcString = $"Factorial {n} = {result}"
        };
        _repository.AddCalculation(calculation);
    }

    public void addSimpleMathToDb(int a, int b, string mathOperator, double result)
    {
        var calculation = new Calculation()
        {
            CalculationId = Guid.NewGuid(),
            CalcString = $"{a}{mathOperator}{b}={result}"
            
        };
        _repository.AddCalculation(calculation);
    }
    
    public bool IsPrime(int candidate)
    {
        bool result = PrimeCheck(candidate);
        var calculation = new Calculation()
        {
            CalculationId = Guid.NewGuid(),
            CalcString = $"Is {candidate} a prime number {result}"
        };
        _repository.AddCalculation(calculation);
        return result;
    }

    public static bool PrimeCheck(int candidate)
    {
        if (candidate < 2)
        {
            return false;
        }

        for (int divisor = 2; divisor <= Math.Sqrt(candidate); ++divisor)
        {
            if (candidate % divisor == 0)
            {
                return false;
            }
        }
        return true;
       
    }


}