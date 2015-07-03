using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Service;
using System.Net;

namespace ScrumToPractice.Web.Areas.Practice.Controllers
{
    public class ResultController : Controller
    {
        private ISimuladoCortesia cortesia;

        public ResultController()
        {
            cortesia = new CortesiaSimulado();
        }

        // GET: Practice/Result
        public ActionResult Index(int idCortesia)
        {
            // obtem a cortesia
            var resultado = cortesia.GetResultado(idCortesia);

            if (resultado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            return View(resultado);
        }
    }
}