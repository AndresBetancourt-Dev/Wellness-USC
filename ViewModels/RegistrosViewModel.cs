using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Wellness_USC.Areas.Identity.Data;
using Wellness_USC.Models;

namespace Wellness_USC.ViewModels
{
    public class RegistrosViewModel
    {
        public string UserId { get; set; }
        public int ClaseId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Clase { get; set; }

    }
}