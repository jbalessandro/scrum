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
        private IBaseService<CorResposta> serviceCorResposta;
        private IBaseService<CorSimulado> serviceSimulado;
        private ICorSimulado simuladoCor;
        private IParametro parametro;

        public CortesiaSimulado()
        {
            serviceCortesia = new CortesiaService();
            serviceResposta = new RespostaService();
            serviceCorResposta = new CorRespostaService();
            serviceSimulado = new CorSimuladoService();
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

        /// <summary>
        /// Retorna proxima questao
        /// </summary>
        /// <param name="idSimulado"></param>
        /// <returns></returns>
        public QuestaoCortesia GetProximaQuestao(int idSimulado)
        {
            var simulado = serviceSimulado.Find(idSimulado);

            if (simulado == null)
            {
                return null;
            }

            return GetProximaQuestao(simulado.IdCortesia, simulado.IdQuestao);
        }

        /// <summary>
        /// Retorna proxima questao
        /// </summary>
        /// <param name="idCortesia"></param>
        /// <param name="idQuestaoAtual"></param>
        /// <returns></returns>
        public QuestaoCortesia GetProximaQuestao(int idCortesia, int idQuestaoAtual = 0)
        {
            // lista de questoes do simulado
            var questoes =  serviceSimulado.Listar().Where(x => x.IdCortesia == idCortesia).OrderBy(x => x.Id).ToList();

            if (idQuestaoAtual == 0 && questoes.Count > 0)
            {
                // a questao atual nao foi informada, retorna a primeira questao do simulado
                return GetQuestao(questoes.First().Id);
            }
            else
            {
                // questao atual
                var questaoAtual = questoes.Where(x => x.Id == idQuestaoAtual).FirstOrDefault();
                var posicao = questoes.IndexOf(questaoAtual);
                if (posicao == questoes.Count - 1)
                {
                    throw new ArgumentException("This is already last question!");
                }
                return GetQuestao(questoes[posicao + 1].Id);
            }
        }

        /// <summary>
        /// Retorna questao anterior
        /// </summary>
        /// <param name="idSimulado"></param>
        /// <returns></returns>
        public QuestaoCortesia GetQuestaoAnterior(int idSimulado)
        {
            var simulado = serviceSimulado.Find(idSimulado);

            if (simulado == null)
            {
                return null;
            }

            return GetQuestaoAnterior(simulado.IdCortesia, simulado.IdQuestao);
        }

        /// <summary>
        /// Retorna questao anterior
        /// </summary>
        /// <param name="idCortesia"></param>
        /// <param name="idQuestaoAtual"></param>
        /// <returns></returns>
        public QuestaoCortesia GetQuestaoAnterior(int idCortesia, int idQuestaoAtual)
        {
            // lista de questoes do simulado
            var questoes = serviceSimulado.Listar().Where(x => x.IdCortesia == idCortesia).OrderBy(x => x.Id).ToList();

            // questao atual
            var questaoAtual = questoes.Where(x => x.Id == idQuestaoAtual).FirstOrDefault();
            var posicao = questoes.IndexOf(questaoAtual);
            if (posicao == 0)
            {
                throw new ArgumentException("This is already first question!");
            }
            return GetQuestao(questoes[posicao - 1].Id);
        }

        /// <summary>
        /// Retorna uma questao do simulado (CorSimulado)
        /// </summary>
        /// <param name="idSimulado"></param>
        /// <returns></returns>
        public QuestaoCortesia GetQuestao(int idSimulado)
        {
            var simulado = serviceSimulado.Find(idSimulado);

            if (simulado == null)
            {
                return null;
            }

            return GetQuestao(simulado.IdCortesia, simulado.IdQuestao);
        }

        /// <summary>
        /// Retorna uma questao do simulado (CorSimulado)
        /// </summary>
        /// <param name="idCortesia"></param>
        /// <param name="idQuestao"></param>
        /// <returns></returns>
        public QuestaoCortesia GetQuestao(int idCortesia, int idQuestao)
        {
            // simulados desta cortesia
            List<CorSimulado> simulados;
            simulados = serviceSimulado.Listar().Where(x => x.IdCortesia == idCortesia).OrderBy(x => x.Id).ToList();
            if (simulados.Count() == 0)
            {
                // gera um novo simulado
                simulados = simuladoCor.GetSimulados(idCortesia).OrderBy(x => x.Id).ToList();
            }
            
            var atual = simulados.Where(x => x.IdQuestao == idQuestao).FirstOrDefault();

            var questao = new QuestaoCortesia();
            questao.QuestaoUsuario = atual;
            questao.NumQuestoesTotal = simulados.Count();
            questao.NumQuestaoAtual = simulados.IndexOf(atual) + 1;
            questao.PrimeiraQuestao = (questao.NumQuestaoAtual == 1);
            questao.UltimaQuestao = (questao.NumQuestaoAtual == questao.NumQuestoesTotal);
            questao.QuestoesNaoRespondidas = simulados.Where(x => x.ResponderDepois == true).OrderBy(x => x.Id).AsEnumerable();
            
            return questao;
        }

        /// <summary>
        /// Grava resposta assinalada pelo usuario
        /// </summary>
        /// <param name="idCorSimulado"></param>
        /// <param name="selecionadas"></param>
        public void GravarRespostaUsuario(int idCorSimulado, IEnumerable<int> selecionadas)
        {
            // lista de respostas para este simulado
            var respostas = serviceCorResposta.Listar().Where(x => x.IdCorSimulado == idCorSimulado).ToList();

            for (int i = 0; i < respostas.Count; i++)
            {
                respostas[i].SelecaoUsuario = (selecionadas.Contains(respostas[i].Id));
                serviceCorResposta.Gravar(respostas[i]);
            }

            // define situacao da resposta do simulado
            var simulado = serviceSimulado.Find(idCorSimulado);
            simulado.ResponderDepois = (selecionadas.Count() == 0);
            serviceSimulado.Gravar(simulado);
        }

        /// <summary>
        /// Assinala a questao atual para ser respondida depois e retorna a proxima questao (ou anterior)
        /// </summary>
        /// <param name="idCortesia"></param>
        /// <param name="idQuestao"></param>
        /// <returns></returns>
        public QuestaoCortesia ResponderDepois(int idCortesia, int idQuestao)
        {
            var questao = GetQuestao(idCortesia, idQuestao);

            if (questao != null)
            {
                questao.QuestaoUsuario.ResponderDepois = true;
                serviceSimulado.Gravar(questao.QuestaoUsuario);
            }

            if (questao.UltimaQuestao == false)
            {
                // se nao for a ultima questao retorna a proxima
                return GetProximaQuestao(questao.QuestaoUsuario.IdCortesia, questao.QuestaoUsuario.IdQuestao);
            }
            else
            {
                // retorna a questao anterior
                return GetQuestaoAnterior(questao.QuestaoUsuario.IdCortesia, questao.QuestaoUsuario.IdQuestao);
            }
        }

        /// <summary>
        /// Resultado do simulado
        /// </summary>
        /// <param name="idCortesia"></param>
        /// <returns></returns>
        public CortesiaResultado GetResultado(int idCortesia)
        {
            // simulado
            var cortesia = serviceCortesia.Find(idCortesia);

            if (cortesia != null)
            {
                var resultado = new CortesiaResultado();
                resultado.Cortesia = cortesia;
                resultado.Questoes = serviceSimulado.Listar().Where(x => x.IdCortesia == idCortesia).AsEnumerable();
                resultado.Resultado = GetCorrecao(resultado.Questoes.ToList());
                return resultado;
            }

            return null;
        }

        /// <summary>
        /// Corrige o simulado e retorna o resultado
        /// </summary>
        /// <param name="questoes"></param>
        /// <returns></returns>
        private decimal GetCorrecao(List<CorSimulado> questoes)
        {
            for (int i = 0; i < questoes.Count; i++)
            {
                questoes[i].Correto = (questoes[i].RespostasUsuario.Where(x => x.SelecaoUsuario != x.SelecaoSistema).Count() == 0);
                serviceSimulado.Gravar(questoes[i]);
            }

            Decimal questoesCorretas = Convert.ToDecimal(questoes.Where(x => x.Correto == true).Count());
            Decimal resultado = ((questoesCorretas / Convert.ToDecimal(questoes.Count)) * 100);
            return (resultado);
        }
    }
}