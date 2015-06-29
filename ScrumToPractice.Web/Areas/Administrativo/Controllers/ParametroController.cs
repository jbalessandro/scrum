using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Service;
using System.Net;

namespace ScrumToPractice.Web.Areas.Administrativo.Controllers
{
    public class ParametroController : Controller
    {
        private IBaseService<Parametro> service;

        public ParametroController()
        {
            service = new ParametroService();
        }

        // GET: Administrativo/Parametro
        public ActionResult Index()
        {
            var parametros = service.Listar()
                .OrderBy(x => x.Codigo)
                .AsEnumerable();

            return View(parametros);
        }

        // GET: Administrativo/Parametro/Details/5
        public ActionResult Details(int id)
        {
            var parametro = service.Find(id);

            if (parametro == null)
            {
                return HttpNotFound();
            }

            return View(parametro);
        }

        // GET: Administrativo/Parametro/Create
        public ActionResult Create()
        {
            return View(new Parametro());
        }

        // POST: Administrativo/Parametro/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="Codigo,Valor")] Parametro parametro)
        {
            try
            {
                parametro.Ativo = true;
                parametro.AlteradoEm = DateTime.Now;
                parametro.AlteradoPor = 1; // TODO: usuario
                TryUpdateModel(parametro);

                if (ModelState.IsValid)
	            {
		            service.Gravar(parametro);
                    return RedirectToAction("Index");
	            }
                return View(parametro);
            }
            catch
            {
                return View();
            }
        }

        // GET: Administrativo/Parametro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var parametro = service.Find((int)id);

            if (parametro == null)
            {
                return HttpNotFound();
            }

            return View(parametro);
        }

        // POST: Administrativo/Parametro/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include="Id,Codigo,Valor")] Parametro parametro)
        {
            try
            {
                parametro.AlteradoEm = DateTime.Now;
                parametro.AlteradoPor = 1; // TODO: usuario
                TryUpdateModel(parametro);

                if (ModelState.IsValid)
                {
                    service.Gravar(parametro);
                    return RedirectToAction("Index");
                }
                return View(parametro);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(parametro);
            }
        }

        // GET: Administrativo/Parametro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var parametro = service.Find((int)id);

            if (parametro == null)
            {
                return HttpNotFound();
            }

            return View(parametro);
        }

        // POST: Administrativo/Parametro/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
