using Microsoft.EntityFrameworkCore;
using VUTIS2.DAL.Entities;

namespace VUTIS2.DAL
{
    public class SchoolDbContext : DbContext
    {
        private readonly bool _seedDemoData;

        public SchoolDbContext(DbContextOptions contextOptions, bool seedDemoData = false)
            : base(contextOptions) =>
            _seedDemoData = seedDemoData;

        public DbSet<StudentEntity> Students => Set<StudentEntity>();
        public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();
        public DbSet<SubjectEntity> Subjects => Set<SubjectEntity>();
        public DbSet<EvaluationEntity> Evaluations => Set<EvaluationEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentEntity>()
                .HasMany<EnrollmentsEntity>()
                .WithOne(i => i.Student)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SubjectEntity>()
                .HasMany<EnrollmentsEntity>()
                .WithOne(i => i.Subject)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SubjectEntity>()
                .HasMany<ActivityEntity>()
                .WithOne(i => i.Subject)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ActivityEntity>()
                .HasOne<SubjectEntity>()
                .WithMany(i => i.Activities)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ActivityEntity>()
                .HasOne<EvaluationEntity>();

            //if (_seedDemoData)
            //{
            //    IngredientSeeds.Seed(modelBuilder);
            //    RecipeSeeds.Seed(modelBuilder);
            //    IngredientAmountSeeds.Seed(modelBuilder);
            //}
        }
    }
}
