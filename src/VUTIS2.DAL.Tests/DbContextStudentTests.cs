// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.EntityFrameworkCore;
using VUTIS2.Common.Tests;
using VUTIS2.Common.Tests.Seeds;
using VUTIS2.DAL.Entities;
using Xunit.Abstractions;

namespace VUTIS2.DAL.Tests;

public class DbContextStudentTests(ITestOutputHelper output) : DbContextTestsBase(output)
{
    [Fact]
    public async Task AddNew_Student_Persisted()
    {
        // Arrange
        var entity = StudentSeeds.EmptyStudent with
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Smith",
            PhotoUrl = "https://example.com/photo.jpg",
        };

        // Act
        SchoolDbContextSUT.Students.Add(entity);
        await SchoolDbContextSUT.SaveChangesAsync();

        // Assert
        var actualEntity = await SchoolDbContextSUT.Students
            .SingleOrDefaultAsync(a => a.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }
    [Fact]
    public async Task GetAll_Students()
    {
        // Act
        var entities = await SchoolDbContextSUT.Students.ToListAsync();

        // Assert
        DeepAssert.Contains(StudentSeeds.SampleStudent1 with { Enrollments = Array.Empty<EnrollmentEntity>()}, entities);
        DeepAssert.Contains(StudentSeeds.SampleStudent2 with { Enrollments = Array.Empty<EnrollmentEntity>()}, entities);
    }
}
