using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Repository;

namespace ScrumToPractice.Domain.Service
{
    public class CorSimuladoService: IBaseService<CorSimulado>
    {
        private IBaseRepository<CorSimulado> repository;

        public CorSimuladoService()
        {
            repository = new EFRepository<CorSimulado>();
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

        public IEnumerable<CorSimulado> GetCorSimulado(int idCortesia)
        {
            // lista de questoes simuladas
            var questoes = new QuestaoService().GetCortesia();
           
            foreach (var item in questoes)
            {
                repository.Incluir(new CorSimulado
                {
                    AlteradoEm = DateTime.Now,
                    IdArea = item.IdArea,
                    IdCortesia = idCortesia,
                    IdQuestao = item.Id
                });
            }

            return repository.Listar().Where(x => x.IdCortesia == idCortesia).AsEnumerable();
        }
    }
}
