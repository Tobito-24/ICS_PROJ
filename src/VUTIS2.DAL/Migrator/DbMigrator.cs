using Microsoft.EntityFrameworkCore;
using VUTIS2.DAL.Options;

namespace VUTIS2.DAL.Migrator;

public class DbMigrator(IDbContextFactory<SchoolDbContext> dbContextFactory, DALOptions options)
    : IDbMigrator
{
    public void Migrate() => MigrateAsync(CancellationToken.None).GetAwaiter().GetResult();

    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        await using SchoolDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        if(options.RecreateDatabaseEachTime)
        {
            await dbContext.Database.EnsureDeletedAsync(cancellationToken);
        }
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
    }
}
