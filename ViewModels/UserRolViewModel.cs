using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Wellness_USC.ViewModels
{
    public class UserRolViewModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string Username { get; set; }
        public bool isSelected { get; set; }
    }
}