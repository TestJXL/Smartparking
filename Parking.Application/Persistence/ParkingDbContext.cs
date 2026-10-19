using Microsoft.EntityFrameworkCore;
using Parking.Core.Models;

namespace Parking.Application.Persistence;

public sealed class AppSetting
{
    public string Key { get; set; } = "";
    public string Value { get; set; } = "";
}

public sealed class ParkingDbContext(DbContextOptions<ParkingDbContext> options) : DbContext(options)
{
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<ParkingSpace> Spaces => Set<ParkingSpace>();
    public DbSet<ParkingSession> Sessions => Set<ParkingSession>();
    public DbSet<Lease> Leases => Set<Lease>();
    public DbSet<AppSetting> Settings => Set<AppSetting>();

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<Vehicle>().HasKey(x => x.Carplate);
        model.Entity<ParkingSpace>().HasKey(x => x.Pid);
        model.Entity<ParkingSession>().HasKey(x => x.Sid);
        model.Entity<Lease>().HasKey(x => x.Lid);
        model.Entity<AppSetting>().HasKey(x => x.Key);
        model.Entity<ParkingSpace>().Ignore(x => x.Status);
        model.Entity<ParkingSpace>().HasIndex(x => x.Code).IsUnique();
        // Final database constraint: There cannot be two active parking or rental agreements for the same license plate or parking space.
        model.Entity<ParkingSession>().HasIndex(x => x.Carplate).IsUnique().HasFilter("IsActive = 1");
        model.Entity<ParkingSession>().HasIndex(x => x.Pid).IsUnique().HasFilter("IsActive = 1");
        model.Entity<Lease>().HasIndex(x => x.Carplate).IsUnique().HasFilter("IsActive = 1");
        model.Entity<Lease>().HasIndex(x => x.Pid).IsUnique().HasFilter("IsActive = 1");
        model.Entity<ParkingSession>().HasOne<ParkingSpace>().WithMany().HasForeignKey(x => x.Pid).OnDelete(DeleteBehavior.Restrict);
        model.Entity<Lease>().HasOne<ParkingSpace>().WithMany().HasForeignKey(x => x.Pid).OnDelete(DeleteBehavior.Restrict);
        // Preserve the original -1 sentinel field: Do not create SQL foreign keys for LeaseID/SessionID.
        // These references are managed centrally by the transaction; no direct write interface is provided to the View.
    }
}
