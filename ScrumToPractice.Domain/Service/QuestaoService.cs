using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Repository;

namespace ScrumToPractice.Domain.Service
{
    public class QuestaoService: IBaseService<Questao>, IQuestao 
    {
        private IBaseRepository<Questao> repository;

        public QuestaoService()
        {
            repository = new EFRepository<Questao>();
        }

        public IQueryable<Questao> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Questao item)
        {
            // formata
            item.AlteradoEm = DateTime.Now;
            item.Descricao = item.Descricao.ToUpper().Trim();

            // valida
            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Questão já cadastrada");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }
            else
            {
                return repository.Alterar(item).Id;
            }

        }

        public Questao Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var questao = repository.Find(id);

                if (questao != null)
                {
                    questao.AlteradoEm = DateTime.Now;
                    questao.Ativo = false;
                    return repository.Alterar(questao);
                }

                return questao;
            }
        }

        public Questao Find(int id)
        {
            return repository.Find(id);
        }

        public IEnumerable<Questao> GetQuestoesCortesia(int idCortesia)
        {
            return repository.Listar()
                .Where(x => x.Ativo == true
                    && x.Cortesia == true)
                    .OrderBy(x => Guid.NewGuid())
                    .Take(10)
                    .AsEnumerable(); 
        }

        public IEnumerable<Questao> GetQuestoesSimulado(int idSimulado)
        {
            throw new NotImplementedException();
        }
    }
}
