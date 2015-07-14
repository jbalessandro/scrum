using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Service;
using System.Net;

namespace ScrumToPractice.Web.Areas.Exam.Controllers
{
    public class ResultController : Controller
    {
        private ISimulado _simulado;

        public ResultController()
        {
            _simulado = new SimuladoService();
        }

        // GET: Exam/Result
        public ActionResult Index(int idSimulado)
        {
            // obtem o resultado
            var resultado = _simulado.GetResultado(idSimulado);

            if (resultado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(resultado);
        }
    }
}