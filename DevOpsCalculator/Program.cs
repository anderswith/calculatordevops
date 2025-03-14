using DevOpsCalculator.BLL;
using DevOpsCalculator.BLL.Interfaces;
using DevOpsCalculator.DAL;
using DevOpsCalculator.DAL.Repositories;
using DevOpsCalculator.DAL.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevOpsCalculator;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers();
        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<CalculatorContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
        });
        builder.Services.AddScoped<ICalculatorRepository, CalculatorRepository>();
        builder.Services.AddScoped<ICalculator, CachedCalculator>();
        builder.Services.AddScoped<ICalculator, SimpleCalculator>();
        builder.Services.AddScoped<ICachedCalculator, CachedCalculator>();
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("UserOnly", policy =>
                policy.RequireClaim("EntityType", "User"));
            options.AddPolicy("TechnicianOnly", policy =>
                policy.RequireClaim("EntityType", "Technician"));
        });

        var app = builder.Build();

        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}