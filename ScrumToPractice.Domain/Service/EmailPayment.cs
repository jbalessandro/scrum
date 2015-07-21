using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using System;
using System.Net.Mail;
using System.Text;

namespace ScrumToPractice.Domain.Service
{
    public class EmailPayment: IEmailPayment
    {

        EmailCredential _credential;
        Cliente _cliente;

        public EmailPayment()
        {
            _credential = new EmailCredential();
        }

        public enum StatusEmail
        {
            Enviado,
            Falha
        }

        public StatusEmail EnviarEmail(Cliente cliente)
        {
            // informacoes do cliente
            _cliente = cliente;

            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    // configuracoes para envio
                    smtpClient.EnableSsl = _credential.UsarSsl;
                    smtpClient.Host = _credential.ServidorSmtp;
                    smtpClient.Port = _credential.ServidorPorta;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential(_credential.Sender, _credential.SenderPassword);

                    // body
                    var message = new MailMessage(_credential.Sender, cliente.Email, "ScrumToPractice access key", getMessage());
                    message.IsBodyHtml = true;

                    // envia o email
                    smtpClient.Send(message);
                }
                return StatusEmail.Enviado;
            }
            catch (Exception)
            {
                // nao estoura erro pq desta forma o usuario ira receber de qualquer forma o link na tela
            }

            return StatusEmail.Falha;
        }

        private string getMessage()
        {
            return new StringBuilder()
            .Append("<h3>Welcome to ScrumToPractice</h3>")
            .Append("<br /><br />")
            .Append("This is the link for your practices: ")
            .Append(string.Format("<a href='http://www.scrumtopractice.com/Exam/{0}'>www.scrumtopractice.com/Exam/{0}</a>", _cliente.Chave))
            .Append("<br /><br />")
            .Append("Your access will be valid until ")
            .Append(_cliente.ExpiraEm.ToShortDateString())
            .Append("<br /><br />")
            .Append("Have a good study days!")
            .ToString();
        }
    }
}