using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Service;
using ScrumToPractice.Web.Areas.Administrativo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ScrumToPractice.Web.Areas.Administrativo.Controllers
{
    [Authorize]
    public class RespostaController : Controller
    {
        IBaseService<Resposta> service;
        ILogin login;

        public RespostaController()
        {
            service = new RespostaService();
            login = new UsuarioService();
        }

        // GET: Administrativo/Resposta
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // respostas desta questao
            var respostas = GetRespostas((int)id);
            var questaoRespostas = new QuestaoRespostas
            {
                Questao = new ScrumToPractice.Domain.Service.QuestaoService().Find((int)id),
                Respostas = GetRespostas((int)id)
            };

            return View(questaoRespostas);
        }

        // GET: Administrativo/Resposta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var resposta = service.Find((int)id);

            if (resposta == null)
            {
                return HttpNotFound();
            }

            return View(resposta);
        }

        // GET: Administrativo/Resposta/Create
        public ActionResult Create(int? idQuestao)
        {
            if (idQuestao == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var resposta = new Resposta { IdQuestao = (int)idQuestao };
            return View(resposta);
        }

        // POST: Administrativo/Resposta/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="IdQuestao,Descricao,Correta")] Resposta resposta)
        {
            try
            {
                resposta.AlteradoEm = DateTime.Now;
                resposta.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                resposta.Ativo = true;
                TryUpdateModel(resposta);

                if (ModelState.IsValid)
                {
                    service.Gravar(resposta);
                    return RedirectToAction("Index", new { id = resposta.IdQuestao });
                }
                return View(resposta);
            }
            catch
            {
                return View(resposta);
            }
        }

        // GET: Administrativo/Resposta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var resposta = service.Find((int)id);
            if (resposta == null)
            {
                return HttpNotFound();
            }
            
            return View(resposta);
        }

        // POST: Administrativo/Resposta/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include="Id,IdQuestao,Descricao,Correta,Ativo")] Resposta resposta)
        {
            try
            {
                resposta.AlteradoEm = DateTime.Now;
                resposta.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                TryUpdateModel(resposta);

                if (ModelState.IsValid)
                {
                    service.Gravar(resposta);
                    return RedirectToAction("Index", new { id = resposta.IdQuestao });
                }
                return View(resposta);
            }
            catch
            {
                return View(resposta);
            }
        }

        // GET: Administrativo/Resposta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var resposta = service.Find((int)id);
            if (resposta == null)
            {
                return HttpNotFound();
            }
            return View(resposta);
        }

        // POST: Administrativo/Resposta/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, int? idQuestao)
        {
            try
            {
                if (id == null || idQuestao == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                service.Excluir((int)id);

                return RedirectToAction("Index", new { id = idQuestao });
            }
            catch
            {
                return View();
            }
        }

        private IEnumerable<Resposta> GetRespostas(int idQuestao)
        {
            var respostas = service.Listar().Where(x => x.IdQuestao == idQuestao).AsEnumerable();
            return respostas;
        }
    }
}
