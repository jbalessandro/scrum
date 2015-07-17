using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using SendGrid;
using ScrumToPractice.Domain.Models;

namespace ScrumToPractice.Domain.Service
{
    // https://azure.microsoft.com/pt-br/documentation/articles/sendgrid-dotnet-how-to-send-email/
    public class EmailPayment
    {
        private const bool UsarSsl = false;
        private const string ServidorSmtp = "smtp.sendgrid.net";
        private const int ServidorPorta = 587;
        private const string AzureUser = "azure_bb263be98222aa2b3a0fb13d2554e57a@azure.com";
        private const string AzurePassword = "KmY1CQB43CSCBVQ";
        private const string Sender = "contact@scrumtopractice.com";
        private const string SenderPassword = "b8c7p2c6";

        public enum StatusEmail
        {
            Enviado,
            Falha
        }

        public StatusEmail EnviarEmail(Payment payment)
        {
            // SendGrid - Message
            var message = new SendGridMessage();
            message.From = new MailAddress(Sender);
            message.Subject = "ScrumToPractice access key";
            message.Html = getMessage();
            message.AddTo("jb.alessandro@gmail.com"); // TODO: payment.Property

            // SendGrid - Credentials
            var credential = new System.Net.NetworkCredential(AzureUser, AzurePassword);

            // Web transport for send email
            var transportWeb = new Web(credential);            

            // Send the email
            transportWeb.DeliverAsync(message);

            return StatusEmail.Enviado;
        }

        private string getMessage()
        {
            // TODO: melhorar este texto
            // TODO: incluir a chave de acesso do usuario
            return new StringBuilder()
            .Append("<h3>Welcome to ScrumToPractice</h3>")
            .Append("<br /><br />")
            .Append("Your access goes expire on ")
            .Append("<br /><br />")
            .Append("This is the link for your practices: ")
            .Append("<a href='http://www.scrumtopractice.com/Exam/Exam'>www.scrumtopractice.com/Exam/Exam</a>")
            .Append("<br /><br />")
            .Append("Have a good study days!")
            .ToString();
        }
    }
}