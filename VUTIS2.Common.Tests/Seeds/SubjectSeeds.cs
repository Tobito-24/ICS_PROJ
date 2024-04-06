// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using Microsoft.EntityFrameworkCore;
using VUTIS2.DAL.Entities;

namespace VUTIS2.Common.Tests.Seeds
{
    public static class SubjectSeeds
    {
        public static readonly SubjectEntity EmptySubject = new()
        {
            Id = default,
            Name = default!,
            Abbreviation = default!,
            Activities = default!,
            Students = default!,

        };

        public static readonly SubjectEntity SampleSubject = new()
        {
            Id = Guid.NewGuid(),
            Name = "Seminar C#",
            Abbreviation = "ICS",
            Activities = new List<ActivityEntity> { ActivitySeeds.SampleActivity1 },
            Students = new List<StudentEntity>{ StudentSeeds.SampleStudent1, StudentSeeds.SampleStudent2 },
        };

        public static readonly SubjectEntity SampleSubject2 = new()
        {
            Id = Guid.NewGuid(),
            Name = "Databaze",
            Abbreviation = "IDS",
            Activities = new List<ActivityEntity> { ActivitySeeds.SampleActivity2 },
            Students = new List<StudentEntity> { StudentSeeds.SampleStudent2 },
        };

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubjectEntity>().HasData(
                SampleSubject2,
                SampleSubject,
                EmptySubject
            );
        }
    }
}

