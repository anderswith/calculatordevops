using DevOpsCalculator.BE;
using Microsoft.EntityFrameworkCore;

namespace DevOpsCalculator.DAL;

public class CalculatorContext : DbContext
{
    public DbSet<Calculation> Calculations { get; set; }

    public CalculatorContext(DbContextOptions<CalculatorContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");
        base.OnModelCreating(modelBuilder);
    }
}