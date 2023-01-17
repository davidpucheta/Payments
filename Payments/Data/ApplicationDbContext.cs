using Microsoft.EntityFrameworkCore;
using Payments.Model.Data;

namespace Payments.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Card> Cards { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>().ToTable("Card");
    }
}