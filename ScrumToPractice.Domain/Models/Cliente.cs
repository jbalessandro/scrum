using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrumToPractice.Domain.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Fill your e-mail")]
        [Display(Name="E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage="Fill your name")]
        [Display(Name="Name")]
        public string Nome { get; set; }

        [Display(Name="Since")]
        [Required]
        public DateTime CriadoEm { get; set; }

        public bool Ativo { get; set; }

        [Display(Name="Value")]
        [Required]
        public decimal ValorPago { get; set; }

        [Display(Name="Date of payment")]
        [Required]
        public DateTime PagoEm { get; set; }

        [Display(Name="You can use until")]
        [Required]
        public DateTime ExpiraEm { get; set; }

        [Display(Name="Notes")]
        public string Observacao { get; set; }

        [Display(Name="Cliente Key")]
        [Required]
        public string Chave { get; set; }
    }
}
