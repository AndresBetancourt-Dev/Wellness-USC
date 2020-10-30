using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Wellness_USC.Models
{
    public class Clase
    {
        [Key]
        public int Id_Clase { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public int cantidad { get; set; }
        public string Type { get; set; }
        public int CoursesId { get; set; }

        [ForeignKey("CoursesId")]
        public virtual Course Course { get; set; }
        public int HorarioId { get; set; }
        [ForeignKey("HorarioId")]
        public virtual Horario Horario { get; set; }

        public int ProfesoresId { get; set; }
        [ForeignKey("ProfesoresId")]
        public virtual Profesores Profesores { get; set; }
    }
}
