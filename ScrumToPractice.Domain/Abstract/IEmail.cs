using ScrumToPractice.Domain.Models;

namespace ScrumToPractice.Domain.Abstract
{
    public interface IEmail
    {
        bool Enviar(Contato contato);
    }
}
