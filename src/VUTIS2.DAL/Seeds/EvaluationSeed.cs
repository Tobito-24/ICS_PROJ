using Microsoft.EntityFrameworkCore;
using VUTIS2.DAL.Entities;

namespace VUTIS2.DAL.Seeds
{
    public static class EvaluationSeed
    {
        public static readonly EvaluationEntity EmptyEvaluation = new()
        {
            Id = default,
            Points = default,
            Description = default,
            ActivityId = default,
            Activity = default,
            StudentId = default,
            Student = default
        };

        public static readonly EvaluationEntity EvaluationIMAExam = new()
        {
            Id = Guid.Parse("92dd6d9d-bf28-4c90-9a23-c000b3829943"),
            Points = 80,
            Description = "Passed with flying colors",
            ActivityId = ActivitySeed.ExamIMA.Id,
            Activity = ActivitySeed.ExamIMA,
            StudentId = StudentSeed.StudentB.Id,
            Student = StudentSeed.StudentB
        };

        public static void Seed(this ModelBuilder modelBuilder) =>
            modelBuilder.Entity<EvaluationEntity>().HasData(
                EvaluationIMAExam with { Activity = null!, Student = null! }
            );
    }
}
