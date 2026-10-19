using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Parking.Core.Models;

namespace Parking.Application.Persistence;

// Create a short-lived DbContext for each business operation; do not share tracked entities across pages.
public sealed class EfParkingStore : IDbContextFactory<ParkingDbContext>
{
    private readonly DbContextOptions<ParkingDbContext> _options;
    public EfParkingStore(string databasePath)
    {
        var fullPath = Path.GetFullPath(databasePath);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
        var connection = new SqliteConnectionStringBuilder { DataSource = fullPath, DefaultTimeout = 10 };
        _options = new DbContextOptionsBuilder<ParkingDbContext>().UseSqlite(connection.ToString()).Options;
    }
    public ParkingDbContext CreateDbContext() => new(_options);

    public void Initialize(bool seedDemoSpaces = true, string initialAdminToken = "@admin")
    {
        if (string.IsNullOrWhiteSpace(initialAdminToken) || !initialAdminToken.StartsWith('@'))
            throw new ArgumentException("Administrator tokens must begin with @.", nameof(initialAdminToken));
        using var db = CreateDbContext();
        db.Database.EnsureCreated();
        using var tx = db.Database.BeginTransaction();
        if (!db.Settings.Any(x => x.Key == "AdminToken"))
            db.Settings.Add(new AppSetting { Key = "AdminToken", Value = initialAdminToken });
        if (seedDemoSpaces && !db.Spaces.Any())
        {
            for (var i = 0; i < 12; i++)
                db.Spaces.Add(new ParkingSpace
                {
                    Code = $"A{i + 1:00}", MapRow = i / 4, MapColumn = i % 4,
                    IsBig = i % 4 >= 2, IsEV = i % 2 == 1, Dest = i + 1,
                    IsActive = true, IsFree = true
                });
        }
        db.SaveChanges();
        tx.Commit();
    }
}
