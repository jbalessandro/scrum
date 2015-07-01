using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumToPractice.Domain.Models;

namespace ScrumToPractice.Domain.Abstract
{
    public interface ICorResposta
    {
        CorResposta Find(int idCorSimulado, int idReposta);
        IEnumerable<CorResposta> Listar(int idCorSimulado);
        bool RespostasCorretas(int idCorSimulado);
    }
}
