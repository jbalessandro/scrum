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
        private IBaseService<Resposta> serviceResposta;
        private IBaseService<CorErrado> serviceCorErrado;
        private ICorSimulado simuladoCor;
        private IParametro parametro;

        public CortesiaSimulado()
        {
            serviceCortesia = new CortesiaService();
            serviceResposta = new RespostaService();
            serviceCorErrado = new CorErradoService();
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

        public QuestaoCortesia GetProximaQuestao(int idCortesia, int idQuestaoAtual = 0)
        {
            throw new NotImplementedException();
        }

        public QuestaoCortesia GetQuestaoAnterior(int idCortesia, int idQuestaoAtual)
        {
            throw new NotImplementedException();
        }

        public QuestaoCortesia GetQuestao(int idCortesia, int idQuestao)
        {
            // simulados desta cortesia
            var simulados = simuladoCor.GetSimulados(idCortesia);

            var questao = new QuestaoCortesia();
            questao.QuestaoUsuario = simuladoCor.Find(idCortesia, idQuestao);
            questao.NumQuestoesTotal = simulados.Count() + 1;
            questao.NumQuestaoAtual = simulados.ToList().IndexOf(questao.QuestaoUsuario) + 1;
            questao.RespostaUsuario = GetRespostaUsuario(questao.QuestaoUsuario);
            questao.PrimeiraQuestao = (questao.NumQuestaoAtual == 1);
            
            return questao;
        }

        private IEnumerable<RespostaUsuario> GetRespostaUsuario(CorSimulado corSimulado)
        {
            // possiveis respostas para esta questao
            var respostas = serviceResposta.Listar().Where(x => x.IdQuestao == corSimulado.IdQuestao).AsEnumerable();

            // lista das respostas do usuario
            var lista = new List<RespostaUsuario>();
            foreach (var item in respostas)
            {
                lista.Add(new RespostaUsuario
                {
                    Descricao = item.Descricao,
                    IdResposta = item.Id,
                    Selecionada = GetRespostaSelecionadaPeloUsuario(corSimulado, item)
                });
            }
            return lista;
        }

        private bool GetRespostaSelecionadaPeloUsuario(CorSimulado corSimulado, Resposta resposta)
        {
            if (corSimulado.Correto == true)
            {
                // o usuario selecionou a RESPOSTA para uma RESPOSTA correta
                return true;
            }

            // verifica se esta resposta errada esta cadastradas em CorErrada
            if (serviceCorErrado.Listar().Where(x => x.IdCorSimulado == corSimulado.Id).FirstOrDefault() != null)
            {
                // o usuario selecionou esta RESPOSTA errada, mas selecionou
                return true;
            }

            // a resposta esta errada e o usuario nao selecionou esta opcao (resposta)
            return false;
        }
    }
}