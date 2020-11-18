
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static SweetAlertBlog.Enums.Enums;

namespace Wellness_USC.Controllers
{
    public class TestController : BaseController
    {
        public IActionResult Index()
        {
            Alert("This is a success message", NotificationType.success);
            return View();
        }
    }
}