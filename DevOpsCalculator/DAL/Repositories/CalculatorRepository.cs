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
        return _calcContext.Calculations
            .Select(c => new Calculation
            {
                CalculationId = c.CalculationId,
                CalcString = c.CalcString ?? "", // Ensure non-null string
                A = c.A ?? 0,  // Default to 0 if NULL
                B = c.B ?? 0,  // Default to 0 if NULL
                Result = c.Result ?? 0,  // Default to 0 if NULL
                Operation = c.Operation ?? "Unknown" // Handle null operation
            })
            .ToList();
    }
}