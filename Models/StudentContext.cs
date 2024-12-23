using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Student_Management_API.Models
{
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;
        public StudentContext(DbContextOptions<StudentContext> Options) : base(Options) { }
    }
}
