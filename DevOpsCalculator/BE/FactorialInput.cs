using System.ComponentModel.DataAnnotations;

namespace DevOpsCalculator.BE;

public class FactorialInput
{
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Factorial must be a non-negative integer.")]
    public int N { get; set; }
}