using VUTIS2.BL.Mappers;
using VUTIS2.Common.Tests;
using VUTIS2.Common.Tests.Factories;
using VUTIS2.DAL;
using VUTIS2.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace VUTIS2.BL.Tests;

public class FacadeTestsBase : IAsyncLifetime
{
    protected FacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        // DbContextFactory = new DbContextTestingInMemoryFactory(GetType().Name, seedTestingData: true);
        // DbContextFactory = new DbContextLocalDBTestingFactory(GetType().FullName!, seedTestingData: true);
        DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!, seedTestingData: true);

        StudentModelMapper = new StudentModelMapper();
        EvaluationModelMapper = new EvaluationModelMapper(StudentModelMapper);
        ActivityModelMapper = new ActivityModelMapper(EvaluationModelMapper, SubjectModelMapper);
        SubjectModelMapper = new SubjectModelMapper(StudentModelMapper, ActivityModelMapper);

        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
    }

    protected IDbContextFactory<SchoolDbContext> DbContextFactory { get; }

    protected StudentModelMapper StudentModelMapper{ get; }
    protected EvaluationModelMapper EvaluationModelMapper { get; }
    protected SubjectModelMapper SubjectModelMapper { get; }
    protected ActivityModelMapper ActivityModelMapper { get; }
    protected UnitOfWorkFactory UnitOfWorkFactory { get; }

    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
}
