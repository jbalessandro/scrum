using System.Collections.Generic;

namespace ScrumToPractice.Domain.Models
{
    public class SimuladoCortesia
    {
        public Cortesia Cortesia { get; set; }
        public IEnumerable<CorSimulado> QuestoesSimuladas { get; set; }
    }
}
