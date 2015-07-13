using ScrumToPractice.Domain.Models;
using System.Collections.Generic;

namespace ScrumToPractice.Domain.Service
{
    public interface IQuestao
    {
        IEnumerable<Questao> GetQuestoesCortesia();
        IEnumerable<Questao> GetQuestoesSimulado();
    }
}
