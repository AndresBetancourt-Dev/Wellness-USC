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
        public int Id { get; set; }

        [ForeignKey("Id")]
        public virtual Course Courses { get; set; }
        public int Id_Horario { get; set; }
        [ForeignKey("Id_Horario")]
        public virtual Horario Horarios { get; set; }

        public int Id_Profesores { get; set; }
        [ForeignKey("Id_Profesores")]
        public virtual Profesores Profesoress { get; set; }
    }
}
