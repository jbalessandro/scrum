using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrumToPractice.Domain.Service
{
    public class SimRespostaService: ISimResposta
    {
        private IBaseRepository<SimResposta> repository;

        public SimRespostaService()
        {
            repository = new EFRepository<SimResposta>();
        }

        public IQueryable<SimResposta> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(SimResposta item)
        {
            // valida
            if (item.IdSimQuestao <= 0)
            {
                throw new ArgumentException("Simulado inválido");
            }

            if (item.IdResposta <= 0)
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

        public SimResposta Excluir(int id)
        {
            return repository.Excluir(id);
        }

        public SimResposta Find(int id)
        {
            return repository.Find(id);
        }

        public SimResposta Find(int idSimQuestao, int idResposta)
        {
            return repository.Listar()
                .Where(x => x.IdSimQuestao == idSimQuestao
                && x.IdResposta == idResposta).FirstOrDefault();
        }

        public IEnumerable<SimResposta> Listar(int idSimQuestao)
        {
            return repository.Listar()
                .Where(x => x.IdSimQuestao == idSimQuestao)
                .AsEnumerable();
        }

        public bool RespostasCorretas(int idSimQuestao)
        {
            // lista respostas do usuario para esta questao
            var respostas = repository.Listar()
                .Where(x => x.IdSimQuestao == idSimQuestao)
                .ToList();

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
                // nenhuma resposta para esta questao assinalada pelo usuario
                return false;
            }

            // as alternativas conferem
            return true;
        }
    }
}
