﻿using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Service;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ScrumToPractice.Web.Areas.Administrativo.Controllers
{
    [Authorize]
    public class AreaController : Controller
    {
        private IBaseService<Area> service;
        private ILogin login;

        public AreaController()
        {
            service = new AreaService();
            login = new UsuarioService();
        }

        // GET: Administrativo/Area
        public ActionResult Index()
        {
            var areas = service.Listar().Where(x => x.Ativo == true)
                .OrderBy(x => x.Descricao);

            return View(areas);
        }

        // GET: Administrativo/Area/Details/5
        public ActionResult Details(int id)
        {
            var area = service.Find(id);

            if (area == null)
            {
                return HttpNotFound();
            }

            return View(area);
        }

        // GET: Administrativo/Area/Create
        public ActionResult Create()
        {
            var area = new Area();

            return View(area);
        }

        // POST: Administrativo/Area/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Descricao")] Area area)
        {
            try
            {
                area.AlteradoEm = DateTime.Now;
                area.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);

                if (ModelState.IsValid)
	            {
		            service.Gravar(area);
                    return RedirectToAction("Index");
	            }

                return View(area);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(area);
            }
        }

        // GET: Administrativo/Area/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
	        {
		        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	        }

            var area = service.Find((int)id);

            if (area == null)
	        {
		        return HttpNotFound();
	        }
            
            return View(area);
        }

        // POST: Administrativo/Area/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Descricao,Ativo")] Area area)
        {
            try
            {
                area.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                //area.AlteradoPor = Convert.ToInt32(FormsAuthentication.Decrypt(System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value).UserData);
                area.AlteradoEm = DateTime.Now;

                if (ModelState.IsValid)
                {
                    service.Gravar(area);
                    return RedirectToAction("Index");
                }
                return View(area);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View();
            }
        }

        // GET: Administrativo/Area/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var area = service.Find((int)id);

            if (area == null)
            {
                return HttpNotFound();
            }

            return View(area);
        }

        // POST: Administrativo/Area/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
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
