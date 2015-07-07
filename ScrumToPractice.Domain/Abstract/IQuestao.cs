using ScrumToPractice.Domain.Models;
using System.Collections.Generic;

namespace ScrumToPractice.Domain.Service
{
    public interface IQuestao
    {
        IEnumerable<Questao> GetQuestoesCortesia(int idCortesia);
        IEnumerable<Questao> GetQuestoesSimulado(int idSimulado);
    }
}
