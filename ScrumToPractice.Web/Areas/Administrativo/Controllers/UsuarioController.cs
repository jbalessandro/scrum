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
    public class UsuarioController : Controller
    {
        private IBaseService<Usuario> service;

        public UsuarioController()
        {
            service = new UsuarioService();
        }

        // GET: Administrativo/Usuario
        public ActionResult Index()
        {
            var usuarios = service.Listar().Where(x => x.Ativo == true)
                .OrderBy(x => x.Nome);

            return View(usuarios);
        }

        // GET: Administrativo/Usuario/Details/5
        public ActionResult Details(int id)
        {
            var usuario = service.Find(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // GET: Administrativo/Usuario/Create
        public ActionResult Create()
        {
            var usuario = new Usuario();

            return View(usuario);
        }

        // POST: Administrativo/Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Nome,Email,Login,Senha")] Usuario usuario)
        {
            try
            {
                usuario.CriadoEm = DateTime.Now;

                if (ModelState.IsValid)
                {
                    service.Gravar(usuario);
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(usuario);
            }
        }

        // GET: Administrativo/Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = service.Find((int)id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            usuario.Senha = string.Empty;
            return View(usuario);
        }

        // POST: Administrativo/Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Nome,Email,Login,Senha,Ativo,CriadoEm,ExcluidoEm")] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Gravar(usuario);
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(usuario);
            }
        }

        // GET: Administrativo/Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = service.Find((int)id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // POST: Administrativo/Usuario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
