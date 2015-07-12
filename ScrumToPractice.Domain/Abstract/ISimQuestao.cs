using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Service;
using System.Collections.Generic;

namespace ScrumToPractice.Domain.Abstract
{
    public interface ISimQuestao: IBaseService<SimQuestao>
    {
        IEnumerable<SimQuestao> GetSimulados(int idSimulado);
        SimQuestao Find(int idSimulado, int idQuestao);
    }
}
