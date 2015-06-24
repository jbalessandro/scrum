using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Repository;
using ScrumToPractice.Web.Areas.Administrativo.Models;

namespace ScrumToPractice.Web.Areas.Administrativo.Controllers
{
    public class RespostaController : Controller
    {
        IBaseRepository<Resposta> service;

        public RespostaController()
        {
            service = new EFRepository<Resposta>();
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

        private IEnumerable<Resposta> GetRespostas(int idQuestao)
        {
            var respostas = service.Listar().Where(x => x.IdQuestao == idQuestao).AsEnumerable();
            return respostas;
        }

        // GET: Administrativo/Resposta/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Administrativo/Resposta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrativo/Resposta/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Administrativo/Resposta/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Administrativo/Resposta/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Administrativo/Resposta/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Administrativo/Resposta/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
