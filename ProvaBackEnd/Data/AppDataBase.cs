using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProvaBackEnd.Models;

namespace ProvaBackEnd.Data;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Folha> Folhas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=database.db");
    }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Funcionario>()
            .ToTable("Funcionarios");

        modelBuilder.Entity<Folha>()
            .ToTable("Folhas");

        base.OnModelCreating(modelBuilder);
    }
}
