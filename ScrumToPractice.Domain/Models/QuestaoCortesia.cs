using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumToPractice.Domain.Models
{
    public class QuestaoCortesia
    {
        public CorSimulado QuestaoUsuario { get; set; }        
        public int NumQuestaoAtual { get; set; }
        public int NumQuestoesTotal { get; set; }
    }
}
