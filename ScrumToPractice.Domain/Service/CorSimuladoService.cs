using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrumToPractice.Domain.Service
{
    public class CorSimuladoService: IBaseService<CorSimulado>, ICorSimulado
    {
        private IBaseRepository<CorSimulado> repository;
        private IQuestao questao;
        private IBaseRepository<CorResposta> serviceResposta;
        private IBaseRepository<Questao> serviceQuestao;

        public CorSimuladoService()
        {
            repository = new EFRepository<CorSimulado>();
            questao = new QuestaoService();
            serviceResposta = new EFRepository<CorResposta>();
            serviceQuestao = new EFRepository<Questao>();
        }

        public IQueryable<CorSimulado> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(CorSimulado item)
        {
            // valida
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar()
                .Where(x => x.IdArea == item.IdArea
                && x.IdCortesia == item.IdCortesia 
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

        public CorSimulado Excluir(int id)
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

        public CorSimulado Find(int id)
        {
            return repository.Find(id);
        }

        public IEnumerable<CorSimulado> GetSimulados(int idCortesia)
        {
            // lista de questoes simuladas
            IQuestao questao;
            questao = new QuestaoService();

            var listaSimulados = new List<CorSimulado>();

            foreach (var item in questao.GetQuestoesCortesia())
            {
                var simulado = repository.Incluir(new CorSimulado
                {
                    AlteradoEm = DateTime.Now,
                    IdArea = item.IdArea,
                    IdCortesia = idCortesia,
                    IdQuestao = item.Id
                });

                // adiciona respostas para a questao
                foreach (var resposta in serviceQuestao.Find(item.Id).Respostas.ToList())
                {
                    serviceResposta.Incluir(new CorResposta
                    {
                        IdCorSimulado = simulado.Id,
                        SelecaoSistema = resposta.Correta,
                        IdResposta = resposta.Id
                    });
                }

                listaSimulados.Add(simulado);
            }


            return listaSimulados;
        }

        public CorSimulado Find(int idCortesia, int idQuestao)
        {
            return repository.Listar()
                .Where(x => x.IdCortesia == idCortesia 
                    && x.IdQuestao == idQuestao)
                    .FirstOrDefault();
        }
    }
}
