using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ScrumToPractice.Web.Areas.Administrativo.Models;
using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Service;

namespace ScrumToPractice.Web.Areas.Administrativo.Controllers
{
    public class LoginController : Controller
    {
        // GET: Administrativo/Login
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginUsuario() );
        }

        [HttpPost]
        public ActionResult Index(LoginUsuario loginUsuario, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ILogin login;
                login = new UsuarioService();
                if (login.ValidaLogin(loginUsuario.Login, loginUsuario.Senha))
                {
                    FormsAuthentication.SetAuthCookie(loginUsuario.Login, false);
                    if (Url.IsLocalUrl(returnUrl)
                        && returnUrl.Length > 1
                        && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//")
                        && !returnUrl.StartsWith(@"\//"))
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuário inválido");
                }
            }

            return View(loginUsuario);
        }
    }
}