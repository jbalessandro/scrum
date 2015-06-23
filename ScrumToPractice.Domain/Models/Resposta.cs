using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumToPractice.Domain.Models
{
    public class Resposta
    {
        [Key]
        public int Id { get; set; }

        [Range(1,999999999,ErrorMessage="Questão inválida")]
        public int IdQuestao { get; set; }

        [Required(ErrorMessage="Informe a descrição da alternativa")]
        public string Descricao { get; set; }

        public bool Correta { get; set; }

        public bool Ativo { get; set; }

        [Required]
        public int AlteradoPor { get; set; }

        [Required]
        public DateTime AlteradoEm { get; set; }

        [NotMapped]
        public Questao Questao {
            get
            {
                return new ScrumToPractice.Domain.Service.QuestaoService().Find(IdQuestao);
            }
            set { }
        }

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
