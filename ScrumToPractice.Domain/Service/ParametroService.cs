using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Repository;
using System;
using System.Linq;

namespace ScrumToPractice.Domain.Service
{
    public class ParametroService: IBaseService<Parametro>, IParametro 
    {
        private IBaseRepository<Parametro> repository;
        private const string notaMinima = "NOTA_MINIMA";
        private const string prazoAcessoPago = "PRAZO_ACESSO_PAGO";

        public ParametroService()
        {
            repository = new EFRepository<Parametro>();
        }

        /// <summary>
        /// Lista parametros
        /// </summary>
        /// <returns></returns>
        public IQueryable<Parametro> Listar()
        {
            return repository.Listar();
        }

        /// <summary>
        /// Grava parametro
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Exclui um parametro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Retorna uma parametro a partir do ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Parametro Find(int id)
        {
            return repository.Find(id);
        }

        /// <summary>
        /// Retorna um parametro a partir do codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public Parametro Find(string codigo)
        {
            if (!string.IsNullOrEmpty(codigo))
	        {
                return repository.Listar().Where(x => x.Codigo == codigo).FirstOrDefault();		 
	        }
            return null;
        }

        /// <summary>
        /// Nota minima para passar no exame
        /// </summary>
        /// <returns></returns>
        public decimal GetNotaMinima()
        {
            var parametro = repository.Listar().Where(x => x.Codigo == notaMinima).FirstOrDefault();
            
            if (parametro != null)
            {
                return Convert.ToDecimal(parametro.Valor);   
            }

            return 0M;
        }

        /// <summary>
        /// Prazo de acesso pago em mes
        /// </summary>
        /// <returns></returns>
        public int GetPrazoAcessoPago()
        {
            var parametro = repository.Listar().Where(x => x.Codigo == prazoAcessoPago).FirstOrDefault();

            if (parametro != null)
            {
                return Convert.ToInt32(parametro.Valor);
            }

            return 1;
        }
    }
}
