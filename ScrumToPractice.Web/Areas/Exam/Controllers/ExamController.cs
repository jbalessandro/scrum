using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Service;

namespace ScrumToPractice.Web.Areas.Exam.Controllers
{
    public class ExamController : Controller
    {
        private ISimulado _simulado;
         
        public ExamController()
        {
            _simulado = new SimuladoService();
        }

        // GET: Exam/Exam
        public ActionResult Index(int? idSimulado, QuestaoSimulado questaoSimulado, int? idCliente = 0, string chave = "")
        {
            if (questaoSimulado.NumQuestoesTotal > 0)
            {
                return View(questaoSimulado);
            }

            QuestaoSimulado questao;

            if (idSimulado == null && !string.IsNullOrEmpty(chave))
            {
                questao = _simulado.GetQuestao(_simulado.GetNovoSimulado(_simulado.GetCliente(chave).Id).Id);
            }
            else if (idSimulado == null)
            {
                // cria um novo simulado
                questao = _simulado.GetQuestao(_simulado.GetNovoSimulado((int)idCliente).Id);
            }
            else
            {
                questao = _simulado.GetQuestao((int)idSimulado);
            }

            // retorna view com a primeira questao
            return View(questao);
        }

        public ActionResult Proxima(IEnumerable<int> selecionadas, int idSimulado, int idQuestao)
        {
            // grava resposta usuario para questao
            GravarResposta(idQuestao, selecionadas);

            var proximaQuestao = _simulado.GetProximaQuestao(idSimulado, idQuestao);
            return PartialView("_QuestaoSimulado", (QuestaoSimulado)proximaQuestao);
        }

        public ActionResult Anterior(IEnumerable<int> selecionadas, int idSimulado, int idQuestao)
        {
            // grava resposta do usuario para a questao
            GravarResposta(idQuestao, selecionadas);

            var questaoAnterior = _simulado.GetQuestaoAnterior(idSimulado, idQuestao);
            return PartialView("_QuestaoSimulado", (QuestaoSimulado)questaoAnterior);
        }

        public ActionResult ResponderDepois(int idSimulado, int idQuestao)
        {
            var questao = _simulado.ResponderDepois(idSimulado, idQuestao);
            return PartialView("_QuestaoSimulado", (QuestaoSimulado)questao);
        }

        public ActionResult ExibirQuestao(IEnumerable<int> selecionadas, int idSimulado, int idQuestao, int idQuestaoAtual)
        {
            // grava resposta do usuario para a questao
            GravarResposta(idQuestaoAtual, selecionadas);

            var questao = _simulado.GetQuestao(idSimulado, idQuestao);
            return PartialView("_QuestaoSimulado", (QuestaoSimulado)questao);
        }

        public ActionResult Finish(IEnumerable<int> selecionadas, int idSimulado, int idQuestao)
        {
            // grava resposta do usuario para a questao
            GravarResposta(idQuestao, selecionadas);

            // redireciona para o controller do resultado
            return Json(new { result = "Redirect", url = Url.Action("Index", "Result", new { idSimulado = idSimulado }) });
        }

        private void GravarResposta(int idQuestao, IEnumerable<int> selecionadas)
        {
            _simulado.GravarRespostasUsuario(idQuestao, selecionadas);
        }
    }
}