using AccountService.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Npgsql.NameTranslation;
using EFCore.NamingConventions;

namespace AccountService.Data;

public class DataContextNpgEf : DbContext
{
    public virtual DbSet<UserAccount> UserAccount { get; protected init; }

    public DataContextNpgEf(DbContextOptions options) : base(options)
    {
    
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql().UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserAccount>()
            .ToTable("user_account", "user_schema")
            .HasKey(x => x.Id);
        
    }
}