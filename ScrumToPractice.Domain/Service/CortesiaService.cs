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
    public class CortesiaService : IBaseService<Cortesia>
    {
        private IBaseRepository<Cortesia> repository;
        private ICorSimulado simulado;
        private IParametro serviceParametro;

        public CortesiaService()
        {
            repository = new EFRepository<Cortesia>();
            simulado = new CorSimuladoService();
            serviceParametro = new ParametroService();
        }

        public IQueryable<Cortesia> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Cortesia item)
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

            if (item.NumQuestoes == 0)
            {
                item.NumQuestoes = GetNumQuestoes();
            }

            // grava
            if (item.Id == 0)
            {
                return repository.Incluir(item).Id;
            }
            return repository.Alterar(item).Id;
        }

        public int GetNumQuestoes()
        {
            var parametro = serviceParametro.Find("NUM_QUESTOES_CORTESIA");

            if (parametro != null)
            {
                return Convert.ToInt32(parametro.Valor);
            }
            return 10;
        }

        public Cortesia Excluir(int id)
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

        public Cortesia Find(int id)
        {
            return repository.Find(id);
        }
    }
}
