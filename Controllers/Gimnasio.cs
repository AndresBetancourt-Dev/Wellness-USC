using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wellness_USC.Controllers
{
    public class Gimnasio : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
