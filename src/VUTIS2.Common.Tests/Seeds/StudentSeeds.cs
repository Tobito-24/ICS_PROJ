// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Diagnostics;
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
    };

    public static readonly StudentEntity SampleStudent1 = new()
    {
        Id = Guid.NewGuid(),
        FirstName = "John",
        LastName = "Doe",
        PhotoUrl = "https://example.com/photo.jpg",
    };

    public static readonly StudentEntity SampleStudent2 = new()
    {
        Id = Guid.NewGuid(),
        FirstName = "Jacob",
        LastName = "Done",
        PhotoUrl = "https://example.com/photo.jpg",
    };

    static StudentSeeds()
    {
        SampleStudent1.Subjects?.Add(SubjectSeeds.SampleSubject);
        SampleStudent2.Subjects?.Add(SubjectSeeds.SampleSubject);
        SampleStudent2.Subjects?.Add(SubjectSeeds.SampleSubject2);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentEntity>().HasData(
            SampleStudent1 with { Subjects = null!},
            SampleStudent2 with { Subjects = null!}
        );
    }
}
