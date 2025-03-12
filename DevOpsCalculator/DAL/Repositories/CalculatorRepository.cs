using DevOpsCalculator.BE;
using DevOpsCalculator.DAL.Repositories.interfaces;

namespace DevOpsCalculator.DAL.Repositories;

public class CalculatorRepository : ICalculatorRepository
{
    private readonly CalculatorContext _calcContext;

    public CalculatorRepository(CalculatorContext calculatorContext)
    {
        _calcContext = calculatorContext;
    }

    public void AddCalculation(Calculation calculation)
    {
        _calcContext.Calculations.Add(calculation);
        _calcContext.SaveChanges();
    }

    public IEnumerable<Calculation> GetCalculations()
    {
        var calculations = _calcContext.Calculations.ToList();
        return calculations;
    }
}