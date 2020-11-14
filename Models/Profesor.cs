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
    public class Profesor
    {
        [Key]
        public int ProfesorId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Nombres")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Apellidos")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Nombre Completo")]
        public string FullName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Nombre de Imagen")]
        public string ImageName { get; set; }

        [NotMapped]
        [DisplayName("Subir Imagen")]
        public IFormFile ImageFile { get; set; }


        public virtual List<Clase> Clases { get; set; }
    }
}
