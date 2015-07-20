using System.Web.Mvc;

namespace ScrumToPractice.Web.Areas.Administrativo.Controllers
{
    [Authorize]
    public class HomeAdmController : Controller
    {
        // GET: Administrativo/HomeAdm
        public ActionResult Index(string message ="")
        {
            ViewBag.Message = message;
            return View();
        }
    }
}