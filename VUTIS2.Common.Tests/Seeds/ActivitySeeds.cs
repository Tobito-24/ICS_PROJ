// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VUTIS2.Common.Enums;
using VUTIS2.DAL.Entities;
using VUTIS2.Common.Tests.Seeds;

namespace VUTIS2.Common.Tests.Seeds
{
    public static class ActivitySeeds
    {
        public static readonly ActivityEntity EmptyActivity = new()
        {
            Id = default,
            StartTime = default,
            EndTime = default,
            RoomName = default!,
            ActivityType = default,
            Description = default!,
            SubjectId = default,
            Subject = default,
            EvaluationId = default,
            Evaluation = default!,
        };

        public static readonly ActivityEntity SampleActivity1 = new()
        {
            Id = Guid.NewGuid(),
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddHours(1),
            RoomName = "D105",
            ActivityType = ActivityType.Exam,
            Description = "Polosemestralni zkouska",
            SubjectId = SubjectSeeds.SampleSubject.Id,
            Subject = SubjectSeeds.SampleSubject,
            EvaluationId = Guid.NewGuid(),
            Evaluation = new List<EvaluationEntity>{EvaluationSeeds.SampleEvaluation1, EvaluationSeeds.SampleEvaluation2}
        };

        public static readonly ActivityEntity SampleActivity2 = new()
        {
            Id = Guid.NewGuid(),
            StartTime = DateTime.UtcNow.AddHours(1),
            EndTime = DateTime.UtcNow.AddHours(3),
            RoomName = "E112",
            ActivityType = ActivityType.Exercise,
            Description = "demo cviceni",
            SubjectId = SubjectSeeds.SampleSubject2.Id,
            Subject = SubjectSeeds.SampleSubject2,
            EvaluationId = Guid.NewGuid(),
            Evaluation = new List<EvaluationEntity>()
        };

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivityEntity>().HasData(
                SampleActivity1,
                SampleActivity2,
                EmptyActivity
            );
        }
    }
}

