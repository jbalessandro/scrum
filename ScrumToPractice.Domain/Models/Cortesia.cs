using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrumToPractice.Domain.Models
{
    public class Cortesia
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Date")]
        public DateTime CriadoEm { get; set; }

        [Display(Name="Number of questions")]
        public int NumQuestoes { get; set; }

        [Display(Name="Finished")]
        public bool Concluido { get; set; }

        [Display(Name="Passível de conclusão")]
        public bool Concluir { get; set; }
    }
}
