using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ScrumToPractice.Domain.Models
{
    public class Resposta
    {
        [Key]
        public int Id { get; set; }

        [Range(1,999999999,ErrorMessage="Questão inválida")]
        [HiddenInput(DisplayValue=false)]
        public int IdQuestao { get; set; }

        [Required(ErrorMessage="Informe a descrição da alternativa")]
        [Display(Name="Resposta")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }

        [Display(Name="Alternativa correta?")]
        public bool Correta { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [Display(Name="Alterado por")]
        public int AlteradoPor { get; set; }

        [Required]
        [Display(Name="Alterado em")]
        public DateTime AlteradoEm { get; set; }

        [NotMapped]
        [Display(Name="Questão")]
        public Questao Questao {
            get
            {
                return new ScrumToPractice.Domain.Service.QuestaoService().Find(IdQuestao);
            }
            set { }
        }

        [NotMapped]
        [Display(Name="Usuário")]
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
