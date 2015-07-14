using ScrumToPractice.Domain.Service;
using ScrumToPractice.Domain.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScrumToPractice.Domain.Models
{
    public class CortesiaResultado
    {
        private IParametro _parametro;

        public CortesiaResultado()
        {
            _parametro = new ParametroService();
        }

        public Cortesia Cortesia { get; set; }

        [Display(Name = "Score")]
        public decimal ResultadoAluno { get; set; }

        [Display(Name = "Passing Score")]
        public virtual decimal NotaMinima
        {
            get 
            {
                return _parametro.GetNotaMinima();
            }
            set { }
        }

        [Display(Name = "Result")]
        public virtual bool Resultado {
            get
            {
                return (ResultadoAluno >= NotaMinima);
            }
            set { }
        }

        public IEnumerable<QuestaoCorrigida> Correcao { get; set; }
    }

    public class QuestaoCorrigida
    {
        public CorSimulado Questao { get; set; }
        public IEnumerable<CorResposta> SelecaoAluno { get; set; }
        public IEnumerable<CorResposta> SelecaoSistema { get; set; }
    }
}
