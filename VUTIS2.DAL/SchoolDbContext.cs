using Microsoft.EntityFrameworkCore;
using VUTIS2.DAL.Entities;

namespace VUTIS2.DAL
{
    public class SchoolDbContext(DbContextOptions contextOptions) : DbContext(contextOptions)
    {
        public DbSet<StudentEntity> Students => Set<StudentEntity>();
        public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();
        public DbSet<SubjectEntity> Subjects => Set<SubjectEntity>();
        public DbSet<EvaluationEntity> Evaluations => Set<EvaluationEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentEntity>()
                .HasMany(i => i.Subjects)
                .WithMany(i => i.Students);

            modelBuilder.Entity<SubjectEntity>()
                .HasMany(i => i.Students)
                .WithMany(i => i.Subjects);

            modelBuilder.Entity<SubjectEntity>()
                .HasMany(i => i.Activities)
                .WithOne(i => i.Subject)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ActivityEntity>()
                .HasOne(i => i.Subject)
                .WithMany(i => i.Activities)
                .OnDelete(DeleteBehavior.NoAction);

            //Not sure (0 - many)
            modelBuilder.Entity<ActivityEntity>()
                .HasOne(i => i.Evaluation)
                .WithOne(i => i.Activity)
                .HasForeignKey<EvaluationEntity>(i => i.ActivityId)
                .IsRequired(false);

            //Apparently we don't need to define the relationship between Eval and Activity (it's redundant)

            modelBuilder.Entity<EvaluationEntity>()
                .HasOne(i => i.Student);
        }
    }
}
