using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness_USC.Areas.Identity.Data;

namespace Wellness_USC.Models
{
    public class ClaseDbContext : DbContext
    {
        public ClaseDbContext(DbContextOptions<ClaseDbContext> options)
            : base(options)
        {
        }

        public DbSet<Clase> Clases { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Registro> Registros { get; set; }
        public DbSet<ApplicationUser> AspNetUsers { get; set; }
    }
}
