using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DevOpsCalculator.BE;

public class Calculation
{
    [Key] public Guid CalculationId { get; set; }
    public String? CalcString { get; set; }

    [JsonRequired]
    [Required(ErrorMessage = "A is required")]
    public int? A { get; set; }
    
    [JsonRequired]
    [Required(ErrorMessage = "B is required")]
    public int? B { get; set; }

    public int Result { get; set; }

    public string? Operation { get; }
}