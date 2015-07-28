using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScrumToPractice.Web.Controllers
{
    public class GuideController : Controller
    {
        // GET: Guide
        public ActionResult Index()
        {
            return View();
        }

        // GET: Guide/Read
        public ActionResult Read()
        {
            return View();
        }
    }
}