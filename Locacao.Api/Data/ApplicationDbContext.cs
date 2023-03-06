using Locacao.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Locacao.Api.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
 {
    public DbSet<Produto> Produto { get; private set; }
    public DbSet<Models.Locacao> Locacao { get; private set; }
    public DbSet<Endereco> Endereco { get; private set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
          
        foreach (var property in builder.Model.GetEntityTypes()
                     .SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18, 6)");
        }
    }   
}