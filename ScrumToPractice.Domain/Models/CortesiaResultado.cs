using System.Collections.Generic;

namespace ScrumToPractice.Domain.Models
{
    public class CortesiaResultado
    {
        public Cortesia Cortesia { get; set; }
        public IEnumerable<CorSimulado> Questoes { get; set; }
        public decimal Resultado { get; set; }
    }
}
