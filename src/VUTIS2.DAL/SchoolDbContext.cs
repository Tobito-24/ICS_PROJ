using Microsoft.EntityFrameworkCore;
using VUTIS2.DAL.Entities;
using VUTIS2.DAL.Seeds;

namespace VUTIS2.DAL
{
    public class SchoolDbContext(DbContextOptions contextOptions, bool seedDemoData) : DbContext(contextOptions)
    {
        public DbSet<StudentEntity> Students => Set<StudentEntity>();
        public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();
        public DbSet<SubjectEntity> Subjects => Set<SubjectEntity>();
        public DbSet<EvaluationEntity> Evaluations => Set<EvaluationEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Removed redundant relations (EF core infers them automatically from one side of the relation)
            modelBuilder.Entity<StudentEntity>()
                .HasMany(i => i.Subjects)
                .WithMany(i => i.Students);

            modelBuilder.Entity<SubjectEntity>()
                .HasMany(i => i.Activities)
                .WithOne(i => i.Subject);

            modelBuilder.Entity<ActivityEntity>()
                .HasMany(i => i.Evaluations)
                .WithOne(i => i.Activity);

            modelBuilder.Entity<EvaluationEntity>()
                .HasOne(i => i.Student);


            if (seedDemoData)
            {
                StudentSeeds.Seed(modelBuilder);
                SubjectSeeds.Seed(modelBuilder);
                ActivitySeeds.Seed(modelBuilder);
                EvaluationSeeds.Seed(modelBuilder);
            }
        }
    }
}
