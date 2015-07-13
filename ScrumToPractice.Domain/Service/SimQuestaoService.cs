using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrumToPractice.Domain.Service
{
    public class SimQuestaoService: ISimQuestao
    {
        private IBaseRepository<SimQuestao> repository;

        public SimQuestaoService()
        {
            repository = new EFRepository<SimQuestao>();
        }

        public IEnumerable<SimQuestao> GetSimulados(int idSimulado)
        {
            IQuestao questao;
            IBaseRepository<Questao> serviceQuestao;
            IBaseRepository<SimResposta> serviceResposta;

            questao = new QuestaoService();
            serviceQuestao = new EFRepository<Questao>();
            serviceResposta = new EFRepository<SimResposta>();

            var listaQuestoes = new List<SimQuestao>();

            foreach (var item in questao.GetQuestoesSimulado())
            {
                // adiciona questao ao simulado
                var questaoSimulada = repository.Incluir(new SimQuestao
                {
                    AlteradoEm = DateTime.Now,
                    IdArea = item.IdArea,
                    IdSimulado = idSimulado,
                    IdQuestao = item.Id
                });

                // adiciona respostas para a questao
                foreach (var resposta in serviceQuestao.Find(item.Id).Respostas.ToList())
                {
                    serviceResposta.Incluir(new SimResposta
                    {
                        IdSimQuestao = questaoSimulada.Id,
                        SelecaoSistema = resposta.Correta,
                        IdResposta = resposta.Id
                    });
                    listaQuestoes.Add(questaoSimulada);
                }
            }

            return listaQuestoes;
        }

        public SimQuestao Find(int idSimulado, int idQuestao)
        {
            return repository.Listar()
                .Where(x => x.IdSimulado == idSimulado
                && x.IdQuestao == idQuestao)
                .FirstOrDefault();
                
        }

        public IQueryable<SimQuestao> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(SimQuestao item)
        {
            // valida
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar()
                .Where(x => x.IdArea == item.IdArea
                && x.IdSimulado == item.IdSimulado
                && x.IdQuestao == item.IdQuestao
                && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Questão já cadastrada");
            }

            // grava
            if (item.Id == 0)
            {
                return repository.Incluir(item).Id;
            }
            return repository.Alterar(item).Id;
        }

        public SimQuestao Excluir(int id)
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

        public SimQuestao Find(int id)
        {
            return repository.Find(id);
        }
    }
}
