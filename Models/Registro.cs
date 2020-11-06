using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wellness_USC.Areas.Identity.Data;


namespace Wellness_USC.Models
{
    public class Registro
    {
        [Key]
        public int RegistroId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Id { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser User { get; set; }

        public int ClaseId { get; set; }

        [ForeignKey("ClaseId")]
        public virtual Clase Clase { get; set; }

    }


}