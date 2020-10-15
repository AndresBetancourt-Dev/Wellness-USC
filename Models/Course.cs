using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Wellness_USC.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        public string Type { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Nombre Imagen")]
        public string ImageName { get; set; }
        [NotMapped]
        [DisplayName("Subir Imagen")]
        public IFormFile ImageFile { get; set; }
    }
}