using Microsoft.EntityFrameworkCore;
using VUTIS2.DAL.Entities;

namespace VUTIS2.DAL.Seeds
{
    public static class SubjectSeed
    {
        public static readonly SubjectEntity SubjectIMA = new()
        {
            Id = Guid.Parse("5ece6b37-b4ea-4a85-8ece-ee8beecca847"),
            Name = "Mathematical analysis 1",
            Abbreviation = "IMA1"
        };

        static SubjectSeed()
        {
            SubjectIMA.Activities.Add(ActivitySeed.ExamIMA);
            SubjectIMA.Students.Add(StudentSeed.StudentB);
        }

        public static void Seed(this ModelBuilder modelBuilder) =>
            modelBuilder.Entity<SubjectEntity>().HasData(
                SubjectIMA with { Activities = Array.Empty<ActivityEntity>(), Students = Array.Empty<StudentEntity>() }
            );
    }
}
