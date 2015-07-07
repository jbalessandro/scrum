using System.ComponentModel.DataAnnotations;

namespace ScrumToPractice.Web.Areas.Administrativo.Models
{
    public class LoginUsuario
    {
        [Required]
        [Display(Name="Login")]
        public string Login { get; set; }

        [Required]
        [Display(Name="Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}