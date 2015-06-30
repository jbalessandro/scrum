using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumToPractice.Domain.Models
{
    public class SimuladoCortesia
    {
        public Cortesia Cortesia { get; set; }
        public IEnumerable<CorSimulado> Questoes { get; set; }
    }
}
