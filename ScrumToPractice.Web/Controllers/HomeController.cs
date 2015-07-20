using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Web.Models;
using ScrumToPractice.Domain.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScrumToPractice.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult PostToPaypal()
        {
            IPreco preco;
            preco = new PaypalPreco();

            bool useSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["UseSandBox"]);
            if (useSandbox)
            {
                ViewBag.ActionUrl = "https://www.sandbox.paypal.com/cgi-bin/webscr";
            }
            else
            {
                ViewBag.ActionUrl = "https://www.paypal.com/cgi-bin/webscr";
            }

            Paypal paypal = new Paypal();
            paypal.cmd = "_xclick";
            paypal.business = ConfigurationManager.AppSettings["BusinessAccountKey"];
            paypal.cancel_return = ConfigurationManager.AppSettings["CancelURL"];
            paypal.@return = ConfigurationManager.AppSettings["ReturnURL"]; // + "&PaymentId=1"; can append order Id here
            paypal.notify_url = ConfigurationManager.AppSettings["NotifyURL"]; // +"?PaymentId=1"; to maintain database logic
            paypal.currency_code = ConfigurationManager.AppSettings["CurrencyCode"];
            paypal.item_name = ConfigurationManager.AppSettings["ItemName"];
            paypal.amount = preco.GetPrecoMensal().ToString("N2");

            return View(paypal);            
        }

        [HttpPost]
        public ActionResult RedirectFromPayPal(FormCollection collection)
        {
            Payment payment = new Payment();

            payment.AddressCity = Request.Form["Address_City"].ToString();
            payment.AddressCountry = Request.Form["Address_Country"].ToString();
            payment.AddressCountryCode = Request.Form["Address_Country_Code"].ToString();

            if (!string.IsNullOrEmpty(Request.Form["Contact_Phone"]))
            {
                payment.ContactPhone = Request.Form["Contact_Phone"].ToString();
            }
            else
            {
                payment.ContactPhone = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.Form["First_Name"]))
            {
                payment.FirstName = Request.Form["First_Name"].ToString();
            }
            else
            {
                payment.FirstName = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.Form["Last_Name"]))
            {
                payment.LastName = Request.Form["Last_Name"].ToString();
            }
            else
            {
                payment.LastName = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.Form["Payer_Business_Name"]))
            {
                payment.PayerBusinessName = Request.Form["Payer_Business_Name"].ToString();
            }
            else
            {
                payment.PayerBusinessName = string.Empty;
            }

            payment.PayerEmail = Request.Form["Payer_Email"].ToString();
            payment.PayerId = Request.Form["Payer_Id"].ToString();
            payment.PaymentStatus = Request.Form["Payment_Status"].ToString();
            payment.Tax = Convert.ToDecimal(Request.Form["Tax"].ToString());
            payment.McGross = Convert.ToDecimal(Request.Form["Mc_Gross"]);
            payment.TxnId = Request.Form["Txn_Id"].ToString();

            // inclui pagamento e cliente
            IPagamento pagamento;
            pagamento = new Pagamento();
            payment.IdCliente = pagamento.NovoPagamento(payment);

            var confirmacaoPagamento = new ConfirmacaoPagamento
            {
                Cliente = GetCliente(payment.IdCliente),
                Payment = payment
            };

            return View("Thanks", (ConfirmacaoPagamento)confirmacaoPagamento);
        }

        public ActionResult CancelFromPayPal()
        {
            return RedirectToAction("Index");
        }

        public ActionResult NotifyFromPayPal()
        {
            return View();
        }

        private Cliente GetCliente(int idCliente)
        {
            ICliente cliente;
            cliente = new ClienteService();
            return cliente.Find(idCliente);
        }
    }
}