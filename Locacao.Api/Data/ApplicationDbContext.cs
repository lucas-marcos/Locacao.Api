using Locacao.Api.Models;
using Locacao.Api.Models.TO;
using Microsoft.EntityFrameworkCore;

namespace Locacao.Api.Data;

public class ApplicationDbContext : DbContext
 {
    public DbSet<Login> Login { get; private set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
          
        foreach (var property in builder.Model.GetEntityTypes()
                     .SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18, 6)");
        }
    }   
}