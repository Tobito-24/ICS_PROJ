using Microsoft.EntityFrameworkCore;
using VUTIS2.DAL.Entities;

namespace VUTIS2.DAL.Seeds;
    public static class StudentSeeds
    {
        public static readonly StudentEntity EmptyStudent = new()
        {
            Id = default, FirstName = default!, LastName = default!, PhotoUrl = default,
        };

        public static readonly StudentEntity SampleStudent1 = new()
        {
            Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", PhotoUrl = "https://example.com/photo.jpg",
        };

        public static readonly StudentEntity SampleStudent2 = new()
        {
            Id = Guid.NewGuid(), FirstName = "Jacob", LastName = "Done", PhotoUrl = "https://example.com/photo.jpg",
        };

        static StudentSeeds()
        {
            SampleStudent1.Subjects?.Add(SubjectSeeds.SampleSubject1);
            SampleStudent2.Subjects?.Add(SubjectSeeds.SampleSubject2);
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentEntity>().HasData(
                SampleStudent1 with { Subjects = null! },
                SampleStudent2 with { Subjects = null! }
            );
        }
    }
