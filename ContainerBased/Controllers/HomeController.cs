using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContainerBased.Models;

namespace ContainerBased.Controllers
{
    public class HomeController : Controller
    {
        private MyModel _mm;

        public HomeController(MyModel mm)
        {
            _mm = mm;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
