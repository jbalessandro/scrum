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
                // simulado atual
                var simulado = simuladoCor.Find(idCortesia, idQuestaoAtual);
                if (simulado != null)
                {
                    var questaoAtual = GetQuestao(simulado.Id);
                    if (questaoAtual != null)
                    {
                        if (questaoAtual.UltimaQuestao == true)
                        {
                            throw new ArgumentException("this is the already last question!");
                        }

                        var posicaoAtual = questoes.IndexOf(questoes.Find(x => x.Id == questaoAtual.QuestaoUsuario.Id));
                        if (posicaoAtual >=0 || posicaoAtual < questoes.Count())
                        {
                            return GetQuestao(questoes.ElementAt(posicaoAtual + 1).Id);
                        }
                    }
                }
            }


            return null;
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
            // simulado atual
            var simulado = simuladoCor.Find(idCortesia, idQuestaoAtual);

            if (simulado != null)
            {
                var questaoAtual = GetQuestao(simulado.Id);
                if (questaoAtual != null)
                {
                    if (questaoAtual.PrimeiraQuestao == true)
                    {
                        throw new ArgumentException("This is the already first question!");
                    }
                    
                    // lista de questoes
                    var questoes = serviceSimulado.Listar().Where(x => x.IdCortesia == idCortesia).OrderBy(x => x.Id).ToList();
                    if (questoes.Count() > 0)
                    {
                        var posicaoAtual = questoes.IndexOf(questoes.Find(x => x.Id == questaoAtual.QuestaoUsuario.Id));
                        if (posicaoAtual >0 || posicaoAtual <= questoes.Count())
                        {
                            return GetQuestao(questoes.ElementAt(posicaoAtual-1).Id);
                        }
                    }
                }
            }

            return null;
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
            
            return questao;
        }

    }
}