using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wellness_USC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public ActionResult HttpStatusCodeHandler(int statusCode)
        {

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Error";
                    break;
            }

            return View("NotFound");

        }
    }
}