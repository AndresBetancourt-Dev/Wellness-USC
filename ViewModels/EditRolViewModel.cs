using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wellness_USC.ViewModels
{
    public class EditRolViewModel
    {

        public EditRolViewModel()
        {
            Users = new List<string>();
        }
        public string Id { get; set; }
        [Required(ErrorMessage = "El nombre del Rol debe ser especificado.")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
