using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using System;
using System.Net.Mail;

namespace ScrumToPractice.Domain.Service
{
    public class EnviarEmail: IEmail
    {
        EmailCredential _credential;

        public EnviarEmail()
        {
            _credential = new EmailCredential();
        }

        public bool Enviar(Contato contato)
        {
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

                    // assunto
                    contato.Assunto = "Contato usuario: " + contato.Assunto;
                    contato.Mensagem = string.Format("{0} \n\n Enviada por {1} - {2} em {3}", contato.Mensagem, contato.Nome, contato.Email, DateTime.Now.ToString());
                    var message = new MailMessage(_credential.Sender, _credential.Sender, contato.Assunto, contato.Mensagem);
                    message.IsBodyHtml = false;

                    // envia o email
                    smtpClient.Send(message);

                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
    }
}
