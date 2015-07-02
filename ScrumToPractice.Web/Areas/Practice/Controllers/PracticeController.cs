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
            if (questaoCortesia.NumQuestoesTotal > 0)
            {
                return View(questaoCortesia);
            }

            QuestaoCortesia questao;

            if (idSimulado == null)
            {
                // TODO: voltar aqui
                // cria um novo simulado
                var simulado = cortesia.GetSimulado(cortesia.CriarSimulado());
                //var cortesiaSimulado = cortesia.GetSimulado(simulado.Cortesia.Id);
                //questao = cortesia.GetQuestao(cortesiaSimulado.QuestoesSimuladas.ToList().OrderBy(x => x.Id).First().Id);
                var alternativa = simulado.QuestoesSimuladas.ToList().OrderBy(x => x.Id).First();
                questao = cortesia.GetQuestao(alternativa.IdCortesia, alternativa.IdQuestao);
            }
            else
            {
                questao = cortesia.GetQuestao((int)idSimulado);
            }
           
            // retorna view com a primeira questao
            return View(questao);
        }
        
        public ActionResult Proxima(IEnumerable<int> selecionadas, int idCortesia, int idQuestao)
        {
            // grava resposta usuario para a questao
            GravarResposta(idQuestao, selecionadas);            

            var proximaQuestao = cortesia.GetProximaQuestao(idCortesia, idQuestao);
            return PartialView("_QuestaoCortesia", (ScrumToPractice.Domain.Models.QuestaoCortesia)proximaQuestao);
        }

        public ActionResult Anterior(IEnumerable<int> selecionadas, int idCortesia, int idQuestao)
        {
            // grava resposta usuario para a questao
            GravarResposta(idQuestao, selecionadas);

            var questaoAnterior = cortesia.GetQuestaoAnterior(idCortesia, idQuestao);
            return PartialView("_QuestaoCortesia", (ScrumToPractice.Domain.Models.QuestaoCortesia)questaoAnterior);
        }

        /// <summary>
        /// Grava resposta conforme selecao do usuario
        /// </summary>
        /// <param name="idQuestao"></param>
        /// <param name="selecionadas"></param>
        private void GravarResposta(int idQuestao, IEnumerable<int> selecionadas)
        {
            cortesia.GravarRespostaUsuario(idQuestao, selecionadas);
        }
    }
}