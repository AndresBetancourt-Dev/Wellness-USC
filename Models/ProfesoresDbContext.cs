using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness_USC.Models;

namespace Wellness_USC.Models
{
    public class ProfesoresDbContext : DbContext
    {
        public ProfesoresDbContext(DbContextOptions<ProfesoresDbContext> options)
            : base(options)
        {
        }

        public DbSet<Profesores> Profesoress { get; set; }

        public DbSet<Wellness_USC.Models.Horario> Horario { get; set; }

        public DbSet<Wellness_USC.Models.Clase> Clase { get; set; }
    }
}
