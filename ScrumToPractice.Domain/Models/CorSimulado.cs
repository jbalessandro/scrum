using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrumToPractice.Domain.Models
{
    public class CorSimulado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdCortesia { get; set; }

        [Required]
        public int IdQuestao { get; set; }

        [Required]
        public int IdArea { get; set; }

        [Display(Name="Correct")]
        public bool Correto { get; set; }

        [Display(Name="Answer Later")]
        public bool ResponderDepois { get; set; }

        [Display(Name="Modify")]
        public DateTime AlteradoEm { get; set; }

        [NotMapped]
        public virtual Questao Questao
        {
            get {
                return new ScrumToPractice.Domain.Service.QuestaoService().Find(IdQuestao);
            }
            set { }
        }        

        [NotMapped]
        public virtual Area Area
        {
            get {
                return new ScrumToPractice.Domain.Service.AreaService().Find(IdArea);
            }
            set { }
        }
    }
}
