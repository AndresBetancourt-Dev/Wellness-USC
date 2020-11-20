using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wellness_USC.ViewModels
{
    public class RolViewModel
    {
        [Required]
        public string RoleName { get; set; }
        
    }
}
