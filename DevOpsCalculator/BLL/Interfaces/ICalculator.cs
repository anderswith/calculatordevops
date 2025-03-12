using DevOpsCalculator.BE;

namespace DevOpsCalculator.BLL.Interfaces;

public interface ICalculator
{
    int Add(int a, int b);
    int Subtract(int a, int b);
    int Multiply(int a, int b);
    int Divide(int a, int b);
    int Factorial(int n);
    bool IsPrime(int candidate);
  
}