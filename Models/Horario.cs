using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Wellness_USC.Models
{
    public class Horario
    {
        [Key]
        public int Id_Horario { get; set; }
        [Column(TypeName = "nvarchar(50)")]

        [Display(Name = "Horario")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> release_date { get; set; }

        public virtual IEnumerable<Clase> Clase { get; set; }
    }
}
