using ScrumToPractice.Domain.Models;
using System.Collections.Generic;

namespace ScrumToPractice.Domain.Abstract
{
    public interface ICorResposta
    {
        CorResposta Find(int idCorSimulado, int idResposta);
        IEnumerable<CorResposta> Listar(int idCorSimulado);
        bool RespostasCorretas(int idCorSimulado);
    }
}
