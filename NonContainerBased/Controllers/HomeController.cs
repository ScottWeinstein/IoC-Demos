using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NonContainerBased.Models;

namespace NonContainerBased.Controllers
{
    public class HomeController : Controller
    {
        private MyModel _mm;

        public HomeController()
        {
            _mm = new MyModel();
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _mm.Dispose();
            base.Dispose(disposing);
        }

    }
}
