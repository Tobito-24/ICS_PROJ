using Microsoft.EntityFrameworkCore;
using VUTIS2.Common.Enums;
using VUTIS2.DAL.Entities;

namespace VUTIS2.DAL.Seeds
{
    public static class ActivitySeed
    {
        public static readonly ActivityEntity EmptyActivity = new()
        {
            Id = default,
            StartTime = default,
            EndTime = default,
            RoomName = default,
            ActivityType = default,
            Description = default,
            SubjectId = default,
            Subject = default,

        };

        public static readonly ActivityEntity ExamIMA = new()
        {
            Id = Guid.Parse("2e5ddbaa-a6e1-466b-91c8-38804599e8aa"),
            StartTime = new(2022, 1, 10, 8, 0, 0),
            EndTime = new(2022, 1, 10, 10, 0, 0),
            RoomName = "A1",
            ActivityType = ActivityType.Exam,
            Description = "Math analysis 1 exam",
            SubjectId = SubjectSeed.SubjectIMA.Id,
            Subject = SubjectSeed.SubjectIMA,
        };

        static ActivitySeed()
        {
            ExamIMA.Evaluation?.Add(EvaluationSeed.EvaluationIMAExam);
        }

        public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<SubjectEntity>().HasData(
            ExamIMA with { Subject = null!, Evaluation = null! }
        );
    }
}
