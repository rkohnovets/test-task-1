using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Contractor> Contractors { get; set; }
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contractor>()
            .HasMany(c => c.Contacts)
            .WithOne(e => e.Contractor)
            .HasForeignKey(e => e.ContractorID);

        modelBuilder.Entity<Contractor>()
            .Property(c => c.CreatedAt)
            .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<Contractor>()
            .Property(c => c.UpdatedAt)
            .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<Contact>()
            .Property(c => c.CreatedAt)
            .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<Contact>()
            .Property(c => c.UpdatedAt)
            .HasDefaultValueSql("NOW()");
    }
}