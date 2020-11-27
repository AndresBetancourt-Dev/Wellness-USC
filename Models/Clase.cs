using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Wellness_USC.Models
{
    public class Clase
    {
        [Key]
        public int ClaseId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Nombre de la Clase")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Cantidad")]
        [Range(1, 50,
        ErrorMessage = "La cantidad de estudantes debe estar entre 1 y 50")]
        public int Quantity { get; set; }


        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de Inicio")]
        public Nullable<DateTime> StartDate { get; set; }


        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de Finalización")]
        public Nullable<DateTime> FinishDate { get; set; }

        [Required]
        [Range(0, 23,
        ErrorMessage = "Las hora debe estar entre 0 y 23")]
        [DisplayName("Hora de Inicio")]
        public int StartHour { get; set; }
        [Required]
        [Range(0, 23,
        ErrorMessage = "Las hora debe estar entre 0 y 23")]
        [DisplayName("Hora de Fin")]
        public int EndHour { get; set; }

        public int CursoId { get; set; }

        [ForeignKey("CursoId")]
        [DisplayName("Curso")]
        public virtual Curso Curso { get; set; }

        public int ProfesorId { get; set; }

        [ForeignKey("ProfesorId")]
        [DisplayName("Profesor")]
        public virtual Profesor Profesor { get; set; }

        public virtual List<Registro> Registros { get; set; }
    }

}


