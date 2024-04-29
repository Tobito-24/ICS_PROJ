using Microsoft.EntityFrameworkCore;
using VUTIS2.Common.Enums;
using VUTIS2.DAL.Entities;

namespace VUTIS2.Common.Tests.Seeds;

public static class EnrollmentSeeds
{
    public static readonly EnrollmentEntity EmptyActivity = new()
    {
        Id = default,
        SubjectId = default,
        StudentId = default,
        Student = default!,
        Subject = default!,
    };

    public static readonly EnrollmentEntity SampleEnrollment1 = new()
    {
        Id = Guid.NewGuid(),
        SubjectId = SubjectSeeds.SampleSubject1.Id,
        StudentId = StudentSeeds.SampleStudent1.Id,
        Student = StudentSeeds.SampleStudent1,
        Subject = SubjectSeeds.SampleSubject1,
    };

    public static readonly EnrollmentEntity SampleEnrollment2 = new()
    {
        Id = Guid.NewGuid(),
        SubjectId = SubjectSeeds.SampleSubject2.Id,
        StudentId = StudentSeeds.SampleStudent2.Id,
        Student = StudentSeeds.SampleStudent2,
        Subject = SubjectSeeds.SampleSubject2,
    };
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EvaluationEntity>().HasData(
            SampleEnrollment2,
            SampleEnrollment1
        );
    }
}
