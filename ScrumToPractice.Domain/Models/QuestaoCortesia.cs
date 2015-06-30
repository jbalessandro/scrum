using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumToPractice.Domain.Models
{
    public class QuestaoCortesia
    {
        public int IdCorSimulado { get; set; }
        public int IdCortesia { get; set; }
        public Questao Questao { get; set; }        
        public int NumQuestaoAtual { get; set; }
        public int NumQuestoesTotal { get; set; }
    }
}
