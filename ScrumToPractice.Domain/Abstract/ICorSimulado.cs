using ScrumToPractice.Domain.Models;
using System.Collections.Generic;

namespace ScrumToPractice.Domain.Abstract
{
    public interface ICorSimulado
    {
        IEnumerable<CorSimulado> GetSimulados(int idCortesia);
        CorSimulado Find(int idCortesia, int idQuestao);
    }
}
