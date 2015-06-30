using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;

namespace ScrumToPractice.Domain.Service
{
    public class CortesiaSimulado: ISimuladoCortesia
    {
        private IBaseService<Cortesia> serviceCortesia;
        private ICorSimulado simuladoCor;
        private IParametro parametro;

        public CortesiaSimulado()
        {
            serviceCortesia = new CortesiaService();
            simuladoCor = new CorSimuladoService();
            parametro = new ParametroService();
        }

        /// <summary>
        /// Cria um novo simulado e retorna o Id do simulado criado
        /// </summary>
        /// <returns></returns>
        public int CriarSimulado()
        {
            // gera uma cortesia
            return serviceCortesia.Gravar(new Cortesia());
        }

        /// <summary>
        /// Retorna um simulado com informacoes da Cortesia e das Questoes
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Cortesia e Questoes simuladas</returns>
        public SimuladoCortesia GetSimulado(int id)
        {
            return new SimuladoCortesia
            {
                Cortesia = serviceCortesia.Find(id),
                QuestoesSimuladas = simuladoCor.GetSimulados(id)
            };
        }

        /// <summary>
        /// Numero de questoes definidas como cortesia
        /// </summary>
        /// <returns>Numero de questoes definidas como cortesia, 0 se nao localizar o parametro</returns>
        public int GetNumQuestoes()
        {
            var p = parametro.Find("NUM_QUESTOES_CORTESIA");

            if (p != null)
            {
                return Convert.ToInt32(p.Valor);
            }
            return 0;
        }

        public QuestaoCortesia GetQuestao(int idCortesia, int idQuestao)
        {
            throw new NotImplementedException();
        }

        public QuestaoCortesia GetProximaQuestao(int idCortesia, int idQuestaoAtual = 0)
        {
            throw new NotImplementedException();
        }

        public QuestaoCortesia GetQuestaoAnterior(int idCortesia, int idQuestaoAtual)
        {
            throw new NotImplementedException();
        }
    }
}