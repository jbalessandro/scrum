using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrumToPractice.Domain.Models
{
    public class CorResposta
    {
        [Key]
        public int Id { get; set; }

        public int IdCorSimulado { get; set; }
        public int IdResposta { get; set; }
        public bool SelecaoUsuario { get; set; }
        public bool SelecaoSistema { get; set; }

        [NotMapped]
        public virtual Resposta Resposta
        {
            get {
                return new ScrumToPractice.Domain.Service.RespostaService().Find(IdResposta);
            }
            set { }
        }
    }
}
