using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Wellness_USC.Models
{
    public class Profesores
    {
        [Key]
        public int Id_Profesores { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        public string Apell { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Nombre Imagen")]
        public string ImageNameP { get; set; }
        [NotMapped]
        [DisplayName("Subir Imagen")]
        public IFormFile ImageFileP { get; set; }

        public virtual IEnumerable<Clase> Clase { get; set; }
    }
}
