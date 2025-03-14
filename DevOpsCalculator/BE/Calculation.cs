using System.ComponentModel.DataAnnotations;

namespace DevOpsCalculator.BE;

public class Calculation
{
    [Key]
    public Guid CalculationId { get; set; }
    public String CalcString { get; set; }
    public int? A { get; set; }
    public int? B { get; set; }
    public int Result { get; set; }
    
    public string? Operation { get; }
    
    
    
    
}