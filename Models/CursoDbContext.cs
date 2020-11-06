using Microsoft.EntityFrameworkCore;
using Wellness_USC.Models;

namespace Wellness_USC.Data
{
    public class CursoDbContext : DbContext
    {
        public CursoDbContext(DbContextOptions<CursoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Curso> Cursos { get; set; }
    }
}