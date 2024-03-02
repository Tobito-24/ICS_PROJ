using Microsoft.EntityFrameworkCore;
using VUTIS2.DAL.Entities;

namespace VUTIS2.DAL.Seeds
{
    public static class StudentSeed
    {
        public static readonly StudentEntity StudentA = new()
        {
            Id = Guid.Parse("a54df656-c63c-4cc9-aa25-1760c764974b"),
            FirstName = "Tobiáš",
            LastName = "Adamčík",
            PhotoUrl = "https://upload.wikimedia.org/wikipedia/en/4/4d/Shrek_%28character%29.png"
        };

        public static readonly StudentEntity StudentB = new()
        {
            Id = Guid.Parse("7ca29d7a-ffca-4f6d-91ec-4852d062892e"),
            FirstName = "Adam",
            LastName = "Žluva",
            PhotoUrl = "https://upload.wikimedia.org/wikipedia/en/b/b9/Princess_Fiona.png"
        };

        static StudentSeed()
        {
            StudentB.Subjects.Add(SubjectSeed.SubjectIMA);
        }

        public static void Seed(this ModelBuilder modelBuilder) =>
            modelBuilder.Entity<StudentEntity>().HasData(
                StudentA with { Subjects = Array.Empty<SubjectEntity>() },
                StudentB with { Subjects = Array.Empty<SubjectEntity>() }
            );
    }
}
