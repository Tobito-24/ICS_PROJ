using VUTIS2.Common.Tests;
using VUTIS2.Common.Tests.Factories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace VUTIS2.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{
    protected DbContextTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!, seedTestingData: true);
        SchoolDbContextSUT = DbContextFactory.CreateDbContext();
    }

    protected IDbContextFactory<SchoolDbContext> DbContextFactory { get; }
    protected SchoolDbContext SchoolDbContextSUT { get; }


    public async Task InitializeAsync()
    {
        await SchoolDbContextSUT.Database.EnsureDeletedAsync();
        await SchoolDbContextSUT.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await SchoolDbContextSUT.Database.EnsureDeletedAsync();
        await SchoolDbContextSUT.DisposeAsync();
    }
}
