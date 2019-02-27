using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SecretSanta.Web.Controllers
{
    //By default it looks in root, looks for homecontroller with method called Index. 
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}