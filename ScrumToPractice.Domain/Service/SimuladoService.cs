using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrumToPractice.Domain.Service
{
    public class SimuladoService: ISimulado
    {
        private IBaseRepository<Simulado> repository;
        private IQuestao questaoService;
        private ISimQuestao simQuestaoService;
        private ISimResposta simRespostaService;

        public SimuladoService()
        {
            repository = new EFRepository<Simulado>();
            questaoService = new QuestaoService();
            simQuestaoService = new SimQuestaoService();
            simRespostaService = new SimRespostaService();
        }

        public Simulado GetNovoSimulado(int idCliente)
        {
            // grava um novo simulado
            var id = Gravar(new Simulado
            {
                Concluido = false,
                Concluir = false,
                CriadoEm = DateTime.Now,
                IdCliente = idCliente,
                NumQuestoes = 0,
                QuestaoAtual = 1,
            });

            // adiciona questoes ao simulado
            simQuestaoService.GetSimulados(id);

            return Find(id);
        }

        public QuestaoSimulado GetQuestao(int idSimulado)
        {
            // retorna a primeira questao deste simulado
            return GetProximaQuestao(idSimulado);
        }

        public QuestaoSimulado GetQuestao(int idSimulado, int idQuestao)
        {
            // questoes deste simulado
            var questoes = simQuestaoService.Listar()
                .Where(x => x.IdSimulado == idSimulado)
                .OrderBy(x => x.Id)
                .ToList();

            if (questoes.Count() == 0)
            {
                // gera um novo simulado
                questoes = simQuestaoService.GetSimulados(idSimulado).OrderBy(x => x.Id).ToList();
            }

            var atual = questoes.Where(x => x.IdQuestao == idQuestao).FirstOrDefault();

            var questao = new QuestaoSimulado();
            questao.QuestaoUsuario = atual;
            questao.NumQuestoesTotal = questoes.Count();
            questao.NumQuestaoAtual = questoes.IndexOf(atual) + 1;
            questao.PrimeiraQuestao = (questao.NumQuestaoAtual == 1);
            questao.UltimaQuestao = (questao.NumQuestaoAtual == questao.NumQuestoesTotal);
            questao.QuestoesNaoRespondidas = questoes.Where(x => x.ResponderDepois == true).OrderBy(x => x.Id).AsEnumerable();
            questao.Concluir = Find(idSimulado).Concluir;

            return questao;
        }

        public QuestaoSimulado GetProximaQuestao(int idSimulado, int idQuestaoAtual = 0)
        {
            // lista de questoes
            var questoes = simQuestaoService.Listar()
                .Where(x => x.IdSimulado == idSimulado)
                .OrderBy(x => x.Id)
                .ToList();

            if (idQuestaoAtual == 0 && questoes.Count > 0)
            {
                // a questao atual nao foi informada, retorna a primeira questao
                var primeiraQuestao = questoes.First();
                return GetQuestao(primeiraQuestao.IdSimulado, primeiraQuestao.IdQuestao);
            }
            else
            {
                // questao atual
                var questaoAtual = questoes.Where(x => x.Id == idQuestaoAtual).FirstOrDefault();
                var posicao = questoes.IndexOf(questaoAtual);
                if (posicao == questoes.Count -1)
                {
                    // ja eh a ultima questao, nao ha como ir para a proxima
                    throw new ArgumentException("This is already last question!");
                }

                // obtem a proxima questao
                var proximaQuestao = GetQuestao(questoes[posicao + 1].IdSimulado, questoes[posicao + 1].IdQuestao);

                // se a questao for a ultima do simulado, assinala flag que o simulado pode ser concluido
                if (proximaQuestao.UltimaQuestao == true)
                {
                    // o sistema assina flag permitindo a partir de agora que o simulado seja concluido a qualquer momento
                    var simulado = Find(idSimulado);
                    simulado.Concluir = true;
                    Gravar(simulado);
                    proximaQuestao.Concluir = true;
                }

                return proximaQuestao;
            }
        }

        public QuestaoSimulado GetQuestaoAnterior(int idSimulado, int idQuestaoAtual)
        {
            // lista de questoes
            var questoes = simQuestaoService.Listar()
                .Where(x => x.IdSimulado == idSimulado)
                .OrderBy(x => x.Id)
                .ToList();

            // questao atual
            var questaoAtual = questoes.Where(x => x.Id == idQuestaoAtual).FirstOrDefault();
            var posicao = questoes.IndexOf(questaoAtual);
            if (posicao == 0)
            {
                throw new ArgumentException("This is already first question!");
            }

            var questaoAnterior = questoes[posicao - 1];

            return GetQuestao(questaoAnterior.IdSimulado, questaoAnterior.IdQuestao);
        }

        public QuestaoSimulado ResponderDepois(int idSimulado, int idQuestao)
        {
            var questao = GetQuestao(idSimulado, idQuestao);

            if (questao != null)
            {
                questao.QuestaoUsuario.ResponderDepois = true;
                simQuestaoService.Gravar(questao.QuestaoUsuario);
            }

            if (questao.UltimaQuestao == false)
            {
                // se nao for a ultima questao retorna a proxima
                return GetProximaQuestao(questao.QuestaoUsuario.IdSimulado, questao.QuestaoUsuario.Id);
            }
            else
            {
                // retorna a questao anterior
                return GetQuestaoAnterior(questao.QuestaoUsuario.IdSimulado, questao.QuestaoUsuario.Id);
            }
        }

        public void GravarRespostasUsuario(int idSimQuestao, IEnumerable<int> selecionadas)
        {
            // lista de respostas para este simulado
            var respostas = simRespostaService.Listar()
                .Where(x => x.IdSimQuestao == idSimQuestao)
                .ToList();
            
            if (selecionadas != null)
            {
                for (int i = 0; i < respostas.Count; i++)
                {
                    respostas[i].SelecaoUsuario = (selecionadas.Contains(respostas[i].Id));
                    simRespostaService.Gravar(respostas[i]);
                }
            }

            // define situacao da resposta da questao
            var questao = simQuestaoService.Find(idSimQuestao);
            questao.ResponderDepois = (selecionadas == null);
            simQuestaoService.Gravar(questao);
        }

        public IQueryable<Simulado> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Simulado item)
        {
            // valida
            if (item.Id == 0 || item.CriadoEm == DateTime.MinValue)
            {
                item.CriadoEm = DateTime.Now;
                if (item.Id == 0)
                {
                    item.Concluido = false;
                }
            }

            if (item.IdCliente == 0)
            {
                throw new ArgumentException("Invalid client");
            }

            if (item.NumQuestoes == 0)
            {
                if (item.Id == 0)
                {
                    // numero de questoes que irao compor este simulado
                    item.NumQuestoes = GetNumQuestoes();
                }
                else
                {
                    // numero de questoes que efetivamente compoe este simulado
                    item.NumQuestoes = GetNumQuestoesSimulado(item.Id);
                }
            }

            // grava
            if (item.Id == 0)
            {
                return repository.Incluir(item).Id;
            }
            return repository.Alterar(item).Id;

        }

        private int GetNumQuestoes()
        {
            IBaseService<Questao> questao;
            questao = new QuestaoService();

            return questao.Listar().Where(x => x.Ativo == true).Count();
        }

        private int GetNumQuestoesSimulado(int idSimulado)
        {
            // verifica se ja existem questoes cadastradas para este simulado
            ISimQuestao questaoSimulado;
            questaoSimulado = new SimQuestaoService();

            return questaoSimulado.Listar().Where(x => x.IdSimulado == idSimulado).Count();            
        }

        public Simulado Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK
                return repository.Find(id);
            }
        }

        public Simulado Find(int id)
        {
            return repository.Find(id);
        }

        public SimuladoResultado GetResultado(int idSimulado)
        {
            // simulado
            var simulado = repository.Find(idSimulado);

            if (simulado != null)
            {
                // corrige o simulado
                var questoesCorrigidas = CorrigirSimulado(idSimulado);

                var resultado = new SimuladoResultado();
                resultado.Simulado = simulado;
                resultado.ResultadoAluno = GetResultadoAluno(questoesCorrigidas);
                resultado.Correcao = GetCorrecao(questoesCorrigidas);
                resultado.TotalQuestoes = resultado.Correcao.Count();
                resultado.RespostasCorretas = resultado.Correcao.Where(x => x.Questao.Correto == true).Count();
                resultado.RespostasErradas = resultado.Correcao.Where(x => x.Questao.Correto == false).Count();

                return resultado;
            }

            return null;
        }

        private List<SimQuestao> CorrigirSimulado(int idSimulado)
        {
            // questoes do simulado
            var questoes = simQuestaoService.Listar().Where(x => x.IdSimulado == idSimulado).ToList();

            // corrige o simulado
            for (int i = 0; i < questoes.Count; i++)
            {
                questoes[i].Correto = (questoes[i].RespostasUsuario.Where(x => x.SelecaoUsuario != x.SelecaoSistema).Count() == 0);
                simQuestaoService.Gravar(questoes[i]);
            }

            return questoes;
        }

        private decimal GetResultadoAluno(List<SimQuestao> questoes)
        {
            // retorna o resultado
            decimal questoesCorretas = Convert.ToDecimal(questoes.Where(x => x.Correto == true).Count());
            decimal resultado = ((questoesCorretas / Convert.ToDecimal(questoes.Count)) * 100);
            return resultado;
        }

        private IEnumerable<QuestaoCorrigidaSimulado> GetCorrecao(List<SimQuestao> questoesCorridigas)
        {
            var lista = new List<QuestaoCorrigidaSimulado>();

            foreach (var item in questoesCorridigas.OrderBy(x => x.Area.Descricao))
            {
                lista.Add(new QuestaoCorrigidaSimulado
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
