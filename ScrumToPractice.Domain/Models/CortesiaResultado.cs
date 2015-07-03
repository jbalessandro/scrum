using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumToPractice.Domain.Models
{
    public class CortesiaResultado
    {
        public Cortesia Cortesia { get; set; }
        public IEnumerable<CorSimulado> Questoes { get; set; }
        public decimal Resultado { get; set; }
    }
}
