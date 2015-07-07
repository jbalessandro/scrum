using System;
using System.ComponentModel.DataAnnotations;

namespace ScrumToPractice.Domain.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Informe o nome")]
        [StringLength(100, ErrorMessage="O nome do usuário é composto por no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage="Informe o e-mail")]
        [StringLength(100,ErrorMessage="O e-mail do usuário é composto por no máximo 100 caracteres")]
        [DataType(DataType.EmailAddress)]
        [Display(Name="E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage="Informe o login")]
        [StringLength(20,ErrorMessage="O login é composto por no mínimo 4 caracteres e no máximo 20 caracteres",MinimumLength=4)]        
        public string Login { get; set; }

        [Required(ErrorMessage="Informe a senha do usuário")]
        [StringLength(20,ErrorMessage="A senha é composta por no mínimo 6 caracteres e no máximo 20 caracteres",MinimumLength=6)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [Display(Name="Criado em")]
        public DateTime CriadoEm { get; set; }

        [Display(Name="Excluído em")]
        public DateTime? ExcluidoEm { get; set; }
    }
}
