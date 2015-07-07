using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrumToPractice.Domain.Models
{
    public class Parametro
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Informe o código do parâmetro")]
        [StringLength(100,ErrorMessage="O código do parâmetro é composto por no máximo 100 caracteres")]
        [Display(Name="Código do parâmetro")]
        public string Codigo { get; set; }

        [Required(ErrorMessage="Informe o valor do parâmetro")]
        [StringLength(200,ErrorMessage="O valor do parâmetro é compostro por no máximo 200 caracteres")]
        public string Valor { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [Display(Name="Alterado por")]
        public int AlteradoPor { get; set; }

        [Required]
        [Display(Name="Alterado em")]
        public DateTime AlteradoEm { get; set; }

        [NotMapped]
        public virtual Usuario Usuario
        {
            get
            {
                return new ScrumToPractice.Domain.Service.UsuarioService().Find(AlteradoPor);
            }
            set { }
        }
    }
}
