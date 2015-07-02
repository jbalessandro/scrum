using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Service;
using System.Net;

namespace ScrumToPractice.Web.Areas.Practice.Controllers
{
    public class PracticeController : Controller
    {
        private ISimuladoCortesia cortesia;

        public PracticeController()
        {
            cortesia = new CortesiaSimulado();
        }

        // GET: Practice/Practice
        public ActionResult Index(int? idSimulado, QuestaoCortesia questaoCortesia)
        {
            if (questaoCortesia == null)
            {
                return View(questaoCortesia);
            }

            QuestaoCortesia questao;

            if (idSimulado == null)
            {
                // TODO: voltar aqui
                // cria um novo simulado
                var simulado = cortesia.GetSimulado(cortesia.CriarSimulado());
                var cortesiaSimulado = cortesia.GetSimulado(simulado.Cortesia.Id);
                //questao = cortesia.GetQuestao(cortesiaSimulado.QuestoesSimuladas.ToList().OrderBy(x => x.Id).First().Id);
                var alternativa = cortesiaSimulado.QuestoesSimuladas.ToList().OrderBy(x => x.Id).First();
                questao = cortesia.GetQuestao(alternativa.IdCortesia, alternativa.IdQuestao);
            }
            else
            {
                questao = cortesia.GetQuestao((int)idSimulado);
            }
           
            // retorna view com a primeira questao
            return View(questao);
        }

        //public PartialViewResult Next(int idCortesia, int idQuestao, FormCollection collection)
        public ActionResult Proxima(IEnumerable<int> selecionadas, int idCortesia, int idQuestao)
        {
            var proximaQuestao = cortesia.GetProximaQuestao(idCortesia, idQuestao);
            // TODO: validar proxima questao....
            // add model error
            return PartialView("_QuestaoCortesia", (ScrumToPractice.Domain.Models.QuestaoCortesia)proximaQuestao);
        }
    }
}