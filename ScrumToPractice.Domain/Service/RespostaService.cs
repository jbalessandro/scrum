using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Repository;

namespace ScrumToPractice.Domain.Service
{
    public class RespostaService: IBaseService<Resposta>
    {
        private IBaseRepository<Resposta> repository;

        public RespostaService()
        {
            repository = new EFRepository<Resposta>();
        }

        public IQueryable<Resposta> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Resposta item)
        {
            // formata
            item.AlteradoEm = DateTime.Now;

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

        public Resposta Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var resposta = repository.Find(id);

                if (resposta != null)
                {
                    resposta.AlteradoEm = DateTime.Now;
                    resposta.Ativo = false;
                    return repository.Alterar(resposta);
                }
                return resposta;
            }
        }

        public Resposta Find(int id)
        {
            return repository.Find(id);
        }
    }
}
