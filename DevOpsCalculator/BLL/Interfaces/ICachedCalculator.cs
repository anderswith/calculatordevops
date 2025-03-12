namespace DevOpsCalculator.BLL.Interfaces;

public interface ICachedCalculator
{
    CachedCalculator.Calculation<T>? GetCachedResult<T>(int a, int? b = null, string operation = "");
    
}