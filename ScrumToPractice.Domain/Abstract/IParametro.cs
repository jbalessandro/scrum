using ScrumToPractice.Domain.Models;

namespace ScrumToPractice.Domain.Abstract
{
    public interface IParametro
    {
        Parametro Find(string codigo);
        decimal GetNotaMinima();
        int GetPrazoAcessoPago();
    }
}
