using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Repository;

namespace ScrumToPractice.Domain.Service
{
    public class CorErradoService: IBaseService<CorErrado>
    {
        IBaseRepository<CorErrado> repository;

        public CorErradoService()
        {
            repository = new EFRepository<CorErrado>();
        }

        public IQueryable<CorErrado> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(CorErrado item)
        {
            if (repository.Listar()
                .Where(x => x.IdCorSimulado == item.IdCorSimulado
                && x.IdResposta == item.IdResposta && item.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Resposta desta");
            }

            // grava
            if (item.Id == 0)
            {
                return repository.Incluir(item).Id;
            }
            return repository.Alterar(item).Id;
        }

        public CorErrado Excluir(int id)
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

        public CorErrado Find(int id)
        {
            return repository.Find(id);
        }
    }
}
