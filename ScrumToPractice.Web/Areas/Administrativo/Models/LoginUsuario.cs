using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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