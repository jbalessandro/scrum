using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumToPractice.Domain.Models
{
    public class Questao
    {
        [Key]
        public int Id { get; set; }

        [Range(0,999999999,ErrorMessage="Selecione a área")]
        [Display(Name="Área")]
        public int IdArea { get; set; }

        [Required(ErrorMessage="Informe a descrição da questão")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }

        [Required(ErrorMessage="Informe o comentário Scrum")]
        [Display(Name="Scrum guide")]
        [DataType(DataType.MultilineText)]
        public string ComentarioScrum { get; set; }

        [Display(Name="Questão de multipla escolha?")]
        public bool MultiplaEscolha { get; set; }

        public bool Cortesia { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [Display(Name="Alterado por")]
        public int AlteradoPor { get; set; }

        [Required]
        [Display(Name="Alterado em")]
        public DateTime AlteradoEm { get; set; }

        [NotMapped]
        [Display(Name="Área")]
        public virtual Area Area
        {
            get {
                return new ScrumToPractice.Domain.Service.AreaService().Find(IdArea);
            }
            set { }
        }

        [NotMapped]
        [Display(Name="Alterado por")]
        public virtual Usuario Usuario
        {
            get
            {
                return new ScrumToPractice.Domain.Service.UsuarioService().Find(AlteradoPor);
            }
            set { }
        }

        [NotMapped]
        public virtual IEnumerable<Resposta> Respostas
        {
            get
            {
                return new ScrumToPractice.Domain.Service.RespostaService().Listar()
                    .Where(x => x.IdQuestao == Id).AsEnumerable();
            }
            set { }
        }
    }
}
