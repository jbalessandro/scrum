
namespace ScrumToPractice.Domain.Abstract
{
    public interface ILogin
    {
        bool ValidaLogin(string login, string senha);
        int GetIdUsuario(string login);
    }
}
