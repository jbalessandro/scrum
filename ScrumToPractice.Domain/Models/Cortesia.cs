using System;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Enable to finish exam")]
        public bool Concluir { get; set; }
    }
}
