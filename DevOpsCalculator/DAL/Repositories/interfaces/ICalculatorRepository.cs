using DevOpsCalculator.BE;

namespace DevOpsCalculator.DAL.Repositories.interfaces;

public interface ICalculatorRepository
{
    IEnumerable<Calculation> GetCalculations();
    void  AddCalculation(Calculation calculation);
}