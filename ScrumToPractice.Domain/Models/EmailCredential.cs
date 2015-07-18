
namespace ScrumToPractice.Domain.Models
{
    public class EmailCredential
    {
        public bool UsarSsl = true;
        public string ServidorSmtp = "smtp.gmail.com";
        public int ServidorPorta = 587;
        public string Sender = "contact@scrumtopractice.com";
        public string SenderPassword = "b8c7p2c6";
    }
}
