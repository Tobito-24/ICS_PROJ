// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using Microsoft.EntityFrameworkCore;
using VUTIS2.DAL.Entities;

namespace VUTIS2.Common.Tests.Seeds;
public static class StudentSeeds
{
    public static readonly StudentEntity EmptyStudent = new()
    {
        Id = default,
        FirstName = default!,
        LastName = default!,
        PhotoUrl = default,
        Subjects = default!,
    };

    public static readonly StudentEntity SampleStudent1 = new()
    {
        Id = Guid.NewGuid(),
        FirstName = "John",
        LastName = "Doe",
        PhotoUrl = "https://example.com/photo.jpg",
        Subjects = new List<SubjectEntity> { SubjectSeeds.SampleSubject }
    };

    public static readonly StudentEntity SampleStudent2 = new()
    {
        Id = Guid.NewGuid(),
        FirstName = "Jacob",
        LastName = "Done",
        PhotoUrl = "https://example.com/photo.jpg",
        Subjects = new List<SubjectEntity>{ SubjectSeeds.SampleSubject, SubjectSeeds.SampleSubject2 }
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentEntity>().HasData(
            SampleStudent1,
            SampleStudent2,
            EmptyStudent
        );
    }
}
