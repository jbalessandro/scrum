using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Service;
using ScrumToPractice.Web.Areas.Administrativo.Models;

namespace ScrumToPractice.Web.Areas.Administrativo.Controllers
{
    public class PaypalController : Controller
    {
        IPreco _preco;
        ILogin _login;

        public PaypalController()
        {
            _preco = new PaypalPreco();
            _login = new UsuarioService();
        }

        // GET: Administrativo/Paypal
        public ActionResult ValorAssinaturaMensal()
        {
            var preco = new Preco { ValorMensal = _preco.GetPrecoMensal() };
            return View(preco);
        }

        [HttpPost]
        public ActionResult ValorAssinaturaMensal([Bind(Include="ValorMensal")] Preco preco)
        {
            if (preco.ValorMensal > 0)
            {
                _preco.SetPrecoMensal(preco.ValorMensal, _login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name.ToUpper().Trim()));
                var message = string.Format("Valor da assinatura mensal alterado para {0:c}", preco.ValorMensal);
                return RedirectToAction("Index", "HomeAdm", new { message = message });
            }

            return View(preco);
        }
    }
}