using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScrumToPractice.Web.Areas.Administrativo.Controllers
{
    [Authorize]
    public class HomeAdmController : Controller
    {
        // GET: Administrativo/HomeAdm
        public ActionResult Index()
        {
            return View();
        }
    }
}