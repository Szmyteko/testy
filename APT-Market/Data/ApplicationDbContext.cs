using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using APT_Market.Models;

namespace APT_Market.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options){
    }
    public DbSet<APT_Market.Models.Property> Property { get; set; } = default!;
    public DbSet<APT_Market.Models.Tenant> Tenant { get; set; } = default!;
    public DbSet<APT_Market.Models.Payment> Payment { get; set; } = default!;
    public DbSet<APT_Market.Models.RentalAgreement> RentalAgreement { get; set; } = default!;
    public DbSet<APT_Market.Models.MaintenanceRequest> MaintenanceRequest { get; set; } = default!;
    public DbSet<APT_Market.Models.UsersViewModel> UsersViewModel { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relacja Payment -> Property
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Property)
            .WithMany(pr => pr.Payments)
            .HasForeignKey(p => p.PropertyId)
            .OnDelete(DeleteBehavior.Restrict); // Bez kaskadowego usuwania
    }
}

