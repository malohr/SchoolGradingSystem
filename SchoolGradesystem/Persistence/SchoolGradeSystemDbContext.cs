using Microsoft.EntityFrameworkCore;
using SchoolGradesystem.Models;

namespace SchoolGradesystem.Persistence
{
    public class SchoolGradeSystemDbContext : DbContext
    {
        public SchoolGradeSystemDbContext(DbContextOptions<SchoolGradeSystemDbContext> options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; } 
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}
