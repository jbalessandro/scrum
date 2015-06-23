using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Service;
using ScrumToPractice.Web.Areas.Administrativo.Models;
using System.Net;

namespace ScrumToPractice.Web.Areas.Administrativo.Controllers
{
    public class QuestaoController : Controller
    {
        private IBaseService<Questao> service;

        public QuestaoController()
        {
            service = new QuestaoService();
        }

        // GET: Administrativo/Questao
        public ActionResult Index(int? idArea)
        {
            if (idArea == null)
            {
                idArea = 0;
            }

            var questoes = service.Listar()
                .Where(x => x.Ativo == true)
                .OrderBy(x => x.Descricao).ToList();

            ViewBag.IdArea = idArea;
            return View(new AreaQuestoes {
                Areas =  GetAreas(idArea), 
                Questoes = questoes });
        }

        private static List<SelectListItem> GetAreas(int? idArea)
        {
            IBaseService<Area> areaService = new AreaService();

            var areas = areaService.Listar().Where(x => x.Ativo == true)
                .OrderBy(x => x.Descricao).ToList();

            var areaItens = new List<SelectListItem>();
            foreach (var item in areas)
            {
                areaItens.Add(new SelectListItem { Text = item.Descricao, Value = item.Id.ToString(), Selected = (item.Id == idArea) });
            }
            return areaItens;
        }

        // GET: Administrativo/Questao/Details/5
        public ActionResult Details(int id, int idArea)
        {
            var questao = service.Find(id);

            if (questao == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdArea = idArea;
            return View(questao);
        }

        // GET: Administrativo/Questao/Create
        public ActionResult Create(int ?idArea)
        {
            if (idArea == null)
            {
                idArea = 0;
            }

            AreaQuestao areaQuestao = new AreaQuestao
            {
                Areas = GetAreas(idArea),
                Questao = new Questao()
            };

            ViewBag.IdArea = idArea;
            return View(areaQuestao);
        }

        // POST: Administrativo/Questao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Descricao,ComentarioScrum,MultiplaEscolha")] Questao questao, FormCollection collection)
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

        // GET: Administrativo/Questao/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Administrativo/Questao/Edit/5
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

        // GET: Administrativo/Questao/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Administrativo/Questao/Delete/5
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

        public PartialViewResult Questoes(int? idArea)
        {
            if (idArea == null)
            {
                idArea = 0;
            }

            var questoes = service.Listar().Where(x => (idArea == 0 || x.IdArea == idArea)).ToList();

            ViewBag.IdArea = idArea;
            return PartialView("_Questoes", questoes);
        }


    }
}
