using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Service;
using System.Collections.Generic;

namespace ScrumToPractice.Domain.Abstract
{
    public interface ISimulado: IBaseService<Simulado>
    {
        Simulado GetNovoSimulado(int idCliente);

        QuestaoSimulado GetQuestao(int idSimulado);
        QuestaoSimulado GetQuestao(int idSimulado, int idQuestao);
        QuestaoSimulado GetProximaQuestao(int idSimulado, int idQuestaoAtual = 0);
        QuestaoSimulado GetQuestaoAnterior(int idSimulado, int idQuestaoAtual);
        
        QuestaoSimulado ResponderDepois(int idSimulado, int idQuestao);

        void GravarRespostasUsuario(int idSimQuestao, IEnumerable<int> selecionadas);

        SimuladoResultado GetResultado(int idSimulado);
    }
}
