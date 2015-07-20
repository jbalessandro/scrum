
namespace ScrumToPractice.Domain.Abstract
{
    public interface IPreco
    {
        decimal GetPrecoMensal();
        void SetPrecoMensal(decimal valor, int idUsuario);
    }
}
