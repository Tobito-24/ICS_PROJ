using Microsoft.EntityFrameworkCore;
using VUTIS2.DAL.Entities;
using VUTIS2.DAL.Seeds;

namespace VUTIS2.DAL
{
    public class SchoolDbContext(DbContextOptions contextOptions, bool seedDemoData = false) : DbContext(contextOptions)
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
                .WithOne(i => i.Subject)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ActivityEntity>()
                .HasMany(i => i.Evaluation)
                .WithOne(i => i.Activity);

            modelBuilder.Entity<EvaluationEntity>()
                .HasOne(i => i.Student);

            // REVIEW: Why is it crashing??
            //
            // if (seedDemoData)
            // {
            //     StudentSeed.Seed(modelBuilder);
            //     SubjectSeed.Seed(modelBuilder);
            //     ActivitySeed.Seed(modelBuilder);
            //     EvaluationSeed.Seed(modelBuilder);
            // }
        }
    }
}
