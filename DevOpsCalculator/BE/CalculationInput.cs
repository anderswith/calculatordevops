using System.ComponentModel.DataAnnotations;

namespace DevOpsCalculator.BE;

public class CalculationInput
{
    [Required]
    public int A { get; set; }

    [Required]
    public int B { get; set; }
}