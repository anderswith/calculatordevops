using System.ComponentModel.DataAnnotations;

namespace DevOpsCalculator.BE;

public class PrimeCheckInput
{
    [Required]
    [Range(2, int.MaxValue, ErrorMessage = "Prime check must be for a number greater than 1.")]
    public int Candidate { get; set; }
}