using System.ComponentModel.DataAnnotations;

namespace DevOpsCalculator.BE;

public class Calculation
{
    [Key]
    public Guid CalculationId { get; set; }
    public String CalcString { get; set; }
    
}