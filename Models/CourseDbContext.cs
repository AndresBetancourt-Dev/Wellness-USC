using Microsoft.EntityFrameworkCore;
using Wellness_USC.Models;

namespace Wellness_USC.Data
{
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
    }
}