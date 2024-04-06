// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using VUTIS2.DAL;

namespace VUTIS2.Common.Tests.Factories;
public class DbContextSQLiteTestingFactory(string databaseName, bool seedTestingData = false)
    : IDbContextFactory<SchoolDbContext>
{
    public SchoolDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<SchoolDbContext> builder = new();
        builder.UseSqlite($"Data Source={databaseName};Cache=Shared");

        // builder.LogTo(Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        // builder.EnableSensitiveDataLogging();

        return new SchoolTestingDbContext(builder.Options, seedTestingData);
    }
}
