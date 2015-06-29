using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Repository;

namespace ScrumToPractice.Domain.Service
{
    public class ParametroService: IBaseService<Parametro>
    {
        private IBaseRepository<Parametro> repository;

        public ParametroService()
        {
            repository = new EFRepository<Parametro>();
        }

        public IQueryable<Parametro> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Parametro item)
        {
            // formata
            item.AlteradoEm = DateTime.Now;
            item.Ativo = (item.Id == 0 ? true: item.Ativo);
            item.Codigo = item.Codigo.ToUpper().Trim();

            // valida
            if (repository.Listar().Where(x => x.Codigo == item.Codigo && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Parâmetro já cadastrado");
            }

            // grava
            if (item.Id == 0)
            {
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public Parametro Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var parametro = repository.Find(id);

                if (parametro != null)
                {
                    parametro.Ativo = false;
                    parametro.AlteradoEm = DateTime.Now;
                    return repository.Alterar(parametro);
                }
                return parametro;
            }
        }

        public Parametro Find(int id)
        {
            return repository.Find(id);
        }
    }
}
