using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScrumToPractice.Web.Areas.Administrativo.Models
{
    public class Preco
    {
        [Required]
        [Display(Name="Valor assinatura mensal:")]
        [DataType(DataType.Currency)]
        [Range(1,999)]
        public decimal ValorMensal { get; set; }
    }
}