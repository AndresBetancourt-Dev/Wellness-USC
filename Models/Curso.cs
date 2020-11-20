using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Wellness_USC.Models
{
    public enum Tipo_de_Curso
    {
        Deportivo,
        Cultural
    }
    public class Curso
    {
        [Key]
        public int CursoId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required]
        [DisplayName("Nombre del Curso")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Tipo de Curso")]
        public Tipo_de_Curso Type { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Nombre de Imagen")]
        public string ImageName { get; set; }

        [NotMapped]
        [DisplayName("Subir Imagen")]
        public IFormFile ImageFile { get; set; }

        public virtual List<Clase> Clases { get; set; }
    }
}
