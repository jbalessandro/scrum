using System.Collections.Generic;

namespace ScrumToPractice.Domain.Models
{
    public class SimuladoResultado
    {
        public Simulado Simulado { get; set; }
        public IEnumerable<SimQuestao> Questoes { get; set; }
        public decimal Resultado { get; set; }
    }
}
