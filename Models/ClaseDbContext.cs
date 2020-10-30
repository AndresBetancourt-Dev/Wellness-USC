using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wellness_USC.Models
{
    public class ClaseDbContext : DbContext
    {
        public ClaseDbContext(DbContextOptions<ClaseDbContext> options)
            : base(options)
        {
        }

        public DbSet<Clase> Clase { get; set; }
    }
}
