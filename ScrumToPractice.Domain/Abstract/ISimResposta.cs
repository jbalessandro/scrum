using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumToPractice.Domain.Service;
using ScrumToPractice.Domain.Models;

namespace ScrumToPractice.Domain.Abstract
{
    public interface ISimResposta: IBaseService<SimResposta>
    {
        SimResposta Find(int idSimQuestao, int idResposta);
        IEnumerable<SimResposta> Listar(int idSimQuestao);
        bool RespostasCorretas(int idSimQuestao);
    }
}
