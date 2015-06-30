using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumToPractice.Domain.Models;

namespace ScrumToPractice.Domain.Service
{
    public interface IQuestao
    {
        IEnumerable<Questao> GetQuestoesCortesia(int idCortesia);
        IEnumerable<Questao> GetQuestoesSimulado(int idSimulado);
    }
}
