using ScrumToPractice.Domain.Models;

namespace ScrumToPractice.Domain.Abstract
{
    public interface ILogin
    {
        Usuario ValidaLogin(string login, string senha);
        int GetIdUsuario(string login);
    }
}
