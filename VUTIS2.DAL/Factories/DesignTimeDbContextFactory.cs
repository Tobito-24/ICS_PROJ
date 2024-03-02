using Microsoft.EntityFrameworkCore.Design;

namespace VUTIS2.DAL.Factories;

/// <summary>
/// EF Core CLI migration generation uses this DbContext to create model and migration
/// UsedImplicitly
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SchoolDbContext>
{
    private readonly DbContextSqLiteFactory _dbContextSqLiteFactory = new("school.db", true);

    public SchoolDbContext CreateDbContext(string[] args) => _dbContextSqLiteFactory.CreateDbContext();
}
