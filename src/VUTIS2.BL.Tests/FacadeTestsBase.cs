using VUTIS2.BL.Mappers;
using VUTIS2.Common.Tests;
using VUTIS2.Common.Tests.Factories;
using VUTIS2.DAL;
using VUTIS2.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        EnrollmentModelMapper = new EnrollmentModelMapper();
        StudentModelMapper = new StudentModelMapper(EnrollmentModelMapper);
        EvaluationModelMapper = new EvaluationModelMapper(StudentModelMapper);
        ActivityModelMapper = new ActivityModelMapper(EvaluationModelMapper);
        SubjectModelMapper = new SubjectModelMapper(EnrollmentModelMapper, ActivityModelMapper);

        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
    }

    protected IDbContextFactory<SchoolDbContext> DbContextFactory { get; }

    protected StudentModelMapper StudentModelMapper{ get; }
    protected EvaluationModelMapper EvaluationModelMapper { get; }
    protected SubjectModelMapper SubjectModelMapper { get; }
    protected ActivityModelMapper ActivityModelMapper { get; }
    protected EnrollmentModelMapper EnrollmentModelMapper { get; }
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
