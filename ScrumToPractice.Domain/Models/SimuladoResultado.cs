using ScrumToPractice.Domain.Service;
using ScrumToPractice.Domain.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScrumToPractice.Domain.Models
{
    public class SimuladoResultado
    {
        private IParametro _parametro;

        public SimuladoResultado()
        {
            _parametro = new ParametroService();
        }

        public Simulado Simulado { get; set; }
        public decimal ResultadoAluno { get; set; }
        public virtual decimal NotaMinima
        {
            get 
            {
                return _parametro.GetNotaMinima();
            }
            set { }
        }
        public virtual bool Resultado
        {
            get 
            {
                return (ResultadoAluno >= NotaMinima);
            }
            set { }
        }

        public IEnumerable<QuestaoCorrigidaSimulado> Correcao { get; set; }
        public int RespostasCorretas { get; set; }
        public int RespostasErradas { get; set; }
        public int TotalQuestoes { get; set; }
    }

    public class QuestaoCorrigidaSimulado
    {
        public SimQuestao Questao { get; set; }
        public IEnumerable<SimResposta> SelecaoAluno { get; set; }
        public IEnumerable<SimResposta> SelecaoSistema { get; set; }
    }
}
