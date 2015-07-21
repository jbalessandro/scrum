using System.ComponentModel.DataAnnotations;

namespace ScrumToPractice.Domain.Models
{
    public class Contato
    {
        [Required]
        [Display(Name = "Name")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public string Assunto { get; set; }

        [Required]
        [Display(Name = "Your comments")]
        [DataType(DataType.MultilineText)]
        public string Mensagem { get; set; }
    }
}
