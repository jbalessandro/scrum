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
    [Authorize]
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

            AreaQuestao areaQuestao = GetNovaAreaQuestao(idArea);

            ViewBag.IdArea = idArea;
            return View(areaQuestao);
        }

        // POST: Administrativo/Questao/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Descricao,ComentarioScrum,MultiplaEscolha,Cortesia")] Questao questao, FormCollection collection)
        {
            try
            {
                questao.IdArea = int.Parse(Request.Form["Areas"]);
                questao.AlteradoEm = DateTime.Now;
                questao.Ativo = true;
                questao.AlteradoPor = 1; // TODO
                TryUpdateModel(questao);

                if (ModelState.IsValid)
                {
                    // inclui a questao
                    questao.Id = service.Gravar(questao);                    

                    // rediciona para o cadastramento das respostas para esta questao
                    return RedirectToAction("Create", "Resposta", new { idQuestao = questao.Id });
                }
                var areaQuestao = GetNovaAreaQuestao(questao.IdArea);
                return View(areaQuestao);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var areaQuestao = GetNovaAreaQuestao(int.Parse(Request.Form["Areas"]));
                return View(areaQuestao);
            }
        }

        // GET: Administrativo/Questao/Edit/5
        public ActionResult Edit(int? id, int? idArea)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var questao = service.Find((int)id);

            if (questao == null)
            {
                return HttpNotFound();
            }

            var areaQuestao = GetNovaAreaQuestao(questao.IdArea, questao);
            if (idArea == null)
            {
                idArea = 0;
            }
            ViewBag.IdArea = idArea;
            return View(areaQuestao);
        }

        // POST: Administrativo/Questao/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Descricao,ComentarioScrum,MultiplaEscolha,Cortesia,Ativo")] Questao questao, FormCollection collection, int? idArea)
        {
            try
            {
                questao.AlteradoEm = DateTime.Now;
                questao.AlteradoPor = 1; // TODO
                TryUpdateModel(questao);

                if (ModelState.IsValid)
                {
                    questao.IdArea = int.Parse(Request.Form["ddlArea"]);
                    service.Gravar(questao);
                    return RedirectToAction("Index");
                }
                var areaQuestao = GetNovaAreaQuestao(questao.IdArea, questao);
                if (idArea == null)
                {
                    idArea = 0;
                }
                ViewBag.IdArea = idArea;
                return View(areaQuestao);
            }
            catch (ArgumentException e)
            {
                var areaQuestao = GetNovaAreaQuestao(questao.IdArea, questao);
                if (idArea == null)
                {
                    idArea = 0;
                }
                ViewBag.IdArea = idArea;
                ModelState.AddModelError(string.Empty, e.Message);
                return View(areaQuestao);
            }
        }

        // GET: Administrativo/Questao/Delete/5
        public ActionResult Delete(int? id, int? idArea)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (idArea == null)
            {
                idArea = 0;
            }

            var questao = service.Find((int)id);

            if (questao == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdArea = idArea;
            return View(questao);
        }

        // POST: Administrativo/Questao/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int? idArea)
        {
            try
            {
                service.Excluir(id);

                if (idArea == null)
                {
                    idArea = 0;
                }

                return RedirectToAction("Index", new { idArea = idArea });
            }
            catch
            {
                return View();
            }
        }

        // GET: Administrativo/Questao/Imprimir
        public ActionResult Imprimir(int? id)
        {
            if (id == null)
            {
                id = 0;
            }

            var questoes = service.Listar()
                .Where(x => x.Ativo == true && (id == 0 || x.IdArea == id))
                .ToList()
                .OrderBy(x => x.Area.Descricao)
                .ThenBy(x => x.Descricao)
                .AsEnumerable();

            if (id == 0 || questoes.Count() == 0)
            {
                ViewBag.Area = "";
            }
            else
            {
                ViewBag.Area = " - " + questoes.First().Area.Descricao;
            }

            return View(questoes);
        }


        #region [ Partial ]

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

        #endregion


        #region [ Methods ]

        private AreaQuestao GetNovaAreaQuestao(int? idArea, Questao questao = null)
        {
            if (questao == null)
            {
                questao = new Questao();
            }
            AreaQuestao areaQuestao = new AreaQuestao
            {
                Areas = GetAreas(idArea),
                Questao = questao
            };
            return areaQuestao;
        }

        #endregion

    }
}
