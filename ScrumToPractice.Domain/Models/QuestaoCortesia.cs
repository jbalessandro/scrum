using System.Collections.Generic;

namespace ScrumToPractice.Domain.Models
{
    public class QuestaoCortesia
    {
        public CorSimulado QuestaoUsuario { get; set; }
        public IEnumerable<CorSimulado> QuestoesNaoRespondidas { get; set; }
        public int NumQuestaoAtual { get; set; }
        public int NumQuestoesTotal { get; set; }
        public bool PrimeiraQuestao { get; set; }
        public bool UltimaQuestao { get; set; }
        public bool Concluir { get; set; }
    }
}
