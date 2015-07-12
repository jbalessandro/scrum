using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ScrumToPractice.Domain.Models
{
    public class Simulado
    {
        private ISimQuestao simQuestaoService;

        public Simulado()
        {
            simQuestaoService = new SimQuestaoService();
        }

        [Key]
        public int Id { get; set; }

        public int IdCliente { get; set; }
        public DateTime CriadoEm { get; set; }

        [Display(Name = "Number of questions")]
        public int NumQuestoes { get; set; }

        [Display(Name = "Enable to finish exam")]
        public bool Concluir { get; set; }

        [Display(Name = "Finished")]
        public bool Concluido { get; set; }

        [Display(Name="Current question")]
        public int QuestaoAtual { get; set; }

        public virtual IEnumerable<SimQuestao> Questoes 
        { 
            get
            {
                return simQuestaoService.Listar()
                    .Where(x => x.IdSimulado == Id)
                    .AsEnumerable();                    
            }
            set { }
        }
    }
}
