using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrumToPractice.Domain.Models
{
    public class CorErrado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdCorSimulado { get; set; }

        [Required]
        public int IdResposta { get; set; }

        [Required]
        public bool Selecionado { get; set; }
    }
}
