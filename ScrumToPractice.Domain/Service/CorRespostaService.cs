using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Repository;
using ScrumToPractice.Domain.Abstract;

namespace ScrumToPractice.Domain.Service
{
    public class CorRespostaService: IBaseService<CorResposta>, ICorResposta
    {
        private IBaseRepository<CorResposta> repository;

        public CorRespostaService()
        {
            repository = new EFRepository<CorResposta>();
        }

        public IQueryable<CorResposta> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(CorResposta item)
        {
            // valida
            if (item.IdCorSimulado <= 0)
            {
                throw new ArgumentException("Simulado inválido");
            }

            if (item.IdResposta <=0)
            {
                throw new ArgumentException("Resposta inválida");
            }

            // grava
            if (item.Id == 0)
            {
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public CorResposta Excluir(int id)
        {
            return repository.Excluir(id);
        }

        public CorResposta Find(int id)
        {
            return repository.Find(id);
        }

        public CorResposta Find(int idCorSimulado, int idReposta)
        {
            return repository.Listar()
                .Where(x => x.IdCorSimulado == idCorSimulado
                && x.IdResposta == idReposta).FirstOrDefault();
        }

        public IEnumerable<CorResposta> Listar(int idCorSimulado)
        {
            return repository.Listar().Where(x => x.IdCorSimulado == idCorSimulado).AsEnumerable();
        }

        public bool RespostasCorretas(int idCorSimulado)
        {
            // lista respostas do usuario
            var respostas = repository.Listar().Where(x => x.IdCorSimulado == idCorSimulado).ToList();

            if (respostas.Count > 0)
            {
                foreach (var item in respostas)
                {
                    if (item.SelecaoUsuario != item.SelecaoSistema)
                    {
                        // ha pelo menos uma resposta errada, portanto, ja esta errado a respota para a questao
                        return false;
                    }
                }
            }
            else
            {
                // nenhuma resposta para esta questao
                return false;
            }

            // as alternativas conferem
            return true;
        }
    }
}
