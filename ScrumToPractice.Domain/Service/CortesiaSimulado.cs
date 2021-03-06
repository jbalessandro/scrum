﻿using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    // ja eh a ultima questao, nao ha como ir para a proxima
                    throw new ArgumentException("This is already last question!");
                }

                var proximaQuestao = GetQuestao(questoes[posicao + 1].Id);

                // se a questao for a ultima do simulado, assinala flag que o simulado pode ser concluido
                if (proximaQuestao.UltimaQuestao == true)
                {
                    // o sistema assinala flag permitindo a partir de agora que o simulado seja concluido a qualquer momento                    
                    var cortesia = serviceCortesia.Find(idCortesia);
                    cortesia.Concluir = true;
                    serviceCortesia.Gravar(cortesia);
                    proximaQuestao.Concluir = true;
                }

                return proximaQuestao;
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
            questao.Concluir = serviceCortesia.Find(idCortesia).Concluir;
            
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

            if (selecionadas != null)
            {
                for (int i = 0; i < respostas.Count; i++)
                {
                    respostas[i].SelecaoUsuario = (selecionadas.Contains(respostas[i].Id));
                    serviceCorResposta.Gravar(respostas[i]);
                }                
            }

            // define situacao da resposta do simulado
            var simulado = serviceSimulado.Find(idCorSimulado);
            simulado.ResponderDepois = (selecionadas == null);
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
            var questaoAtual = serviceSimulado.Listar().Where(x => x.Id == idQuestao).FirstOrDefault();

            var questao = GetQuestao(idCortesia, questaoAtual.IdQuestao);

            if (questao != null)
            {
                questao.QuestaoUsuario.ResponderDepois = true;
                serviceSimulado.Gravar(questao.QuestaoUsuario);
            }

            if (questao.UltimaQuestao == false)
            {
                // se nao for a ultima questao retorna a proxima
                return GetProximaQuestao(questao.QuestaoUsuario.IdCortesia, questao.QuestaoUsuario.Id );
            }
            else
            {
                // retorna a questao anterior
                return GetQuestaoAnterior(questao.QuestaoUsuario.IdCortesia, questao.QuestaoUsuario.Id);
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
                // corrige o simulado
                var questoesCorrigidas = CorrigirSimulado(idCortesia);

                var resultado = new CortesiaResultado();
                resultado.Cortesia = cortesia;
                resultado.ResultadoAluno = GetResultadoAluno(questoesCorrigidas);
                resultado.Correcao = GetCorrecao(questoesCorrigidas);
                resultado.TotalQuestoes = resultado.Correcao.Count();
                resultado.RespostasCorretas = resultado.Correcao.Where(x => x.Questao.Correto == true).Count();
                resultado.RespostasErradas = resultado.Correcao.Where(x => x.Questao.Correto == false).Count();
                
                return resultado;
            }

            return null;
        }

        /// <summary>
        /// Corrige o simulado
        /// </summary>
        /// <param name="idCortesia"></param>
        /// <returns></returns>
        private List<CorSimulado> CorrigirSimulado(int idCortesia)
        {
            // questoes do simulado
            var questoes = serviceSimulado.Listar().Where(x => x.IdCortesia == idCortesia).ToList();

            // corrige o simulado
            for (int i = 0; i < questoes.Count; i++)
            {
                questoes[i].Correto = (questoes[i].RespostasUsuario.Where(x => x.SelecaoUsuario != x.SelecaoSistema).Count() == 0);
                serviceSimulado.Gravar(questoes[i]);
            }

            return questoes;
        }

        /// <summary>
        /// Retorna o resultado
        /// </summary>
        /// <param name="questoes"></param>
        /// <returns></returns>
        private decimal GetResultadoAluno(List<CorSimulado> questoes)
        {
            // retorna o resultado
            Decimal questoesCorretas = Convert.ToDecimal(questoes.Where(x => x.Correto == true).Count());
            Decimal resultado = ((questoesCorretas / Convert.ToDecimal(questoes.Count)) * 100);
            return resultado;
        }

        /// <summary>
        /// Lista das questoes corrigidas com respostas SelecaoUsuario / SelecaoSistema
        /// </summary>
        /// <param name="questoesCorrigidas"></param>
        /// <returns></returns>
        private IEnumerable<QuestaoCorrigida> GetCorrecao(List<CorSimulado> questoesCorrigidas)
        {
            var lista = new List<QuestaoCorrigida>();

            foreach (var item in questoesCorrigidas.OrderBy(x => x.Area.Descricao))
            {
                lista.Add(new QuestaoCorrigida
                {
                    Questao = item,
                    SelecaoAluno = item.RespostasUsuario.Where(x => x.SelecaoUsuario == true),
                    SelecaoSistema = item.RespostasUsuario.Where(x => x.SelecaoSistema == true)
                });
            }

            return lista;
        }

    }
}