using Microsoft.EntityFrameworkCore;
using VUTIS2.DAL.Entities;

namespace VUTIS2.DAL.Seeds
{
    public static class SubjectSeeds
    {
        public static readonly SubjectEntity EmptySubject = new()
        {
            Id = default,
            Name = default!,
            Abbreviation = default!,

        };

        public static readonly SubjectEntity SampleSubject1 = new()
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
            SampleSubject1.Activities?.Add(ActivitySeeds.SampleActivity1);
            SampleSubject2.Activities?.Add(ActivitySeeds.SampleActivity2);

            SampleSubject1.Students?.Add(StudentSeeds.SampleStudent1);
            SampleSubject2.Students?.Add(StudentSeeds.SampleStudent2);
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubjectEntity>().HasData(
                SampleSubject2 with { Students = null!, Activities = null! },
                SampleSubject1 with { Students = null!, Activities = null! }
            );
        }
    }
}
