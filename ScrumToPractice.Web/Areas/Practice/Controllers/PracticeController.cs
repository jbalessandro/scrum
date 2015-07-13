using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Service;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ScrumToPractice.Web.Areas.Practice.Controllers
{
    public class PracticeController : Controller
    {
        private ISimuladoCortesia cortesia;
        private IBaseService<CorSimulado> serviceSimulado;

        public PracticeController()
        {
            cortesia = new CortesiaSimulado();
            serviceSimulado = new CorSimuladoService();
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
                // cria um novo simulado
                var simulado = cortesia.GetSimulado(cortesia.CriarSimulado());
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
        
        /// <summary>
        /// Grava a resposta e retorna a proxima
        /// </summary>
        /// <param name="selecionadas"></param>
        /// <param name="idCortesia"></param>
        /// <param name="idQuestao"></param>
        /// <returns></returns>
        public ActionResult Proxima(IEnumerable<int> selecionadas, int idCortesia, int idQuestao)
        {
            // grava resposta usuario para a questao
            GravarResposta(idQuestao, selecionadas);            

            var proximaQuestao = cortesia.GetProximaQuestao(idCortesia, idQuestao);
            return PartialView("_QuestaoCortesia", (QuestaoCortesia)proximaQuestao);
        }

        /// <summary>
        /// Grava resposta e retorna a anterior
        /// </summary>
        /// <param name="selecionadas"></param>
        /// <param name="idCortesia"></param>
        /// <param name="idQuestao"></param>
        /// <returns></returns>
        public ActionResult Anterior(IEnumerable<int> selecionadas, int idCortesia, int idQuestao)
        {
            // grava resposta usuario para a questao
            GravarResposta(idQuestao, selecionadas);

            var questaoAnterior = cortesia.GetQuestaoAnterior(idCortesia, idQuestao);
            return PartialView("_QuestaoCortesia", (QuestaoCortesia)questaoAnterior);
        }

        /// <summary>
        /// Define que esta questao sera respondida depois
        /// </summary>
        /// <param name="idCortesia"></param>
        /// <param name="idQuestao"></param>
        /// <returns>Proxima questao a ser respondida, se nao houver retorna a anterior</returns>
        public ActionResult ResponderDepois(int idCortesia, int idQuestao)
        {
            var questao = cortesia.ResponderDepois(idCortesia, idQuestao);
            return PartialView("_QuestaoCortesia", (QuestaoCortesia)questao);
        }

        /// <summary>
        /// Exibe uma determinada questao
        /// </summary>
        /// <param name="idQuestao"></param>
        /// <returns></returns>
        public ActionResult ExibirQuestao(IEnumerable<int> selecionadas, int idCortesia, int idQuestao, int idQuestaoAtual)
        {
            // grava resposta usuario para a questao
            GravarResposta(idQuestaoAtual, selecionadas);

            var questao = cortesia.GetQuestao(idCortesia, idQuestao);
            return PartialView("_QuestaoCortesia", (QuestaoCortesia)questao);
        }

        /// <summary>
        /// Grava a ultima resposta e redireciona para o controller do resultado
        /// </summary>
        /// <param name="selecionadas"></param>
        /// <param name="idCortesia"></param>
        /// <param name="idQuestao"></param>
        /// <returns></returns>
        public ActionResult Finish(IEnumerable<int> selecionadas, int idCortesia, int idQuestao)
        {
            // grava resposta usuario para a questao
            GravarResposta(idQuestao, selecionadas);

            // redireciona para o controller do resultado
            return Json(new { result = "Redirect", url = Url.Action("Index", "Result", new { idCortesia = idCortesia }) });
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