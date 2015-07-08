using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Service;
using ScrumToPractice.Web.Areas.Administrativo.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace ScrumToPractice.Web.Areas.Administrativo.Controllers
{
    public class LoginController : Controller
    {
        private ILogin _login;

        public LoginController()
        {
            _login = new UsuarioService();
        }

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
                var usuario = _login.ValidaLogin(loginUsuario.Login, loginUsuario.Senha);
                if (usuario != null)
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
                    return RedirectToAction("Index", "HomeAdm");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuário inválido");
                }
            }

            return View(loginUsuario);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}