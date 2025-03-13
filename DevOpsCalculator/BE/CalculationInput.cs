using System.ComponentModel.DataAnnotations;

namespace DevOpsCalculator.BE;

public class CalculationInput
{
    [Required(ErrorMessage = "A is required")]
    public int A { get; set; }

    [Required(ErrorMessage = "B is required")]
    public int B { get; set; }
}