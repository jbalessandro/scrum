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

        [Range(1,999999999,ErrorMessage="Selecione a área")]
        public int IdArea { get; set; }

        [Required(ErrorMessage="Informe a descrição da questão")]
        public string Descricao { get; set; }

        [Required(ErrorMessage="Informe o comentário Scrum")]
        public string ComentarioScrum { get; set; }

        public bool MultiplaEscolha { get; set; }

        public bool Ativo { get; set; }

        [Required]
        public int AlteradoPor { get; set; }

        [Required]
        public DateTime AlteradoEm { get; set; }

        [NotMapped]
        public virtual Area Area
        {
            get {
                return new ScrumToPractice.Domain.Service.AreaService().Find(IdArea);
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
