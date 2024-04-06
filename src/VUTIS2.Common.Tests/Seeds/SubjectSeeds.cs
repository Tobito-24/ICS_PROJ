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

        };

        public static readonly SubjectEntity SampleSubject = new()
        {
            Id = Guid.NewGuid(),
            Name = "Seminar C#",
            Abbreviation = "ICS",
        };

        public static readonly SubjectEntity SampleSubject2 = new()
        {
            Id = Guid.NewGuid(),
            Name = "Databaze",
            Abbreviation = "IDS",
        };

        static SubjectSeeds()
        {
            SampleSubject.Activities?.Add(ActivitySeeds.SampleActivity1);
            SampleSubject2.Activities?.Add(ActivitySeeds.SampleActivity2);

            SampleSubject.Students?.Add(StudentSeeds.SampleStudent1);
            SampleSubject.Students?.Add(StudentSeeds.SampleStudent2);
            SampleSubject2.Students?.Add(StudentSeeds.SampleStudent2);
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubjectEntity>().HasData(
                SampleSubject2 with { Students = null!, Activities = null! },
                SampleSubject with { Students = null!, Activities = null! }
            );
        }
    }
}

