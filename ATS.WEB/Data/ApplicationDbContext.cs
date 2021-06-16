using ATS.WEB.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ATS.WEB.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Cathedra> Cathedras { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<TestResult> TestResults { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
              new IdentityRole(Enums.Role.Admin.ToString()),
              new IdentityRole(Enums.Role.Teacher.ToString()),
              new IdentityRole(Enums.Role.Student.ToString())
            );
            builder.Entity<Question>().HasMany(q => q.Answers).WithOne(a => a.Question).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<TestResult>().HasMany(q => q.Answers).WithOne(a => a.TestResult).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Lesson>().HasMany(q => q.Questions).WithOne(a => a.Lesson).OnDelete(DeleteBehavior.Cascade);
        }


    }
}
