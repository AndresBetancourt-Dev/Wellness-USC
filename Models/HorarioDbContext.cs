using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wellness_USC.Models
{
    public class HorarioDbContext : DbContext
    {
        public HorarioDbContext(DbContextOptions<HorarioDbContext> options)
            : base(options)
        {
        }

        public DbSet<Horario> Horario { get; set; }
    }
}
